using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Shared.Entities;
using System.Diagnostics.Contracts;
using Flunt.Notifications;
using Flunt.Validations;


namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {

            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.IsActive)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .IsGreaterThan(0, subscription.Payments.Count, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
            );
        }
    }
}
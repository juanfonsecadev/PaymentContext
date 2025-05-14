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
            var hasSubscriptionActive = _subscriptions.Any(sub => sub.IsActive);  // Lógica mais simples e direta

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .IsGreaterThan(subscription.Payments.Count, 0, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
            );

            if (IsValid)
            {
                _subscriptions.Add(subscription);
            }
        }
    }
}
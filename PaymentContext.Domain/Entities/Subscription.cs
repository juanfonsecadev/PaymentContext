using PaymentContext.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {

        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            IsActive = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get {return _payments.ToArray(); } }

        public void AddPayment(Payment payment)
        {
             AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(payment.PaidDate, DateTime.Now, "Subscription.Payments", "Data de pagamento inválida")
             );

           
            _payments.Add(payment);
        }

        public void Activate()
        {
            IsActive = true;
            LastUpdateDate = DateTime.Now;
        }
        
        public void Inactivate()
        {
            IsActive = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}
using PaymentContext.Shared.ValueObjects;
using Flunt.Validations;
using Flunt.Notifications;

namespace PaymentContext.Domain.ValueObjetcs
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsEmail(Address, "Email.Address", "E-mail inválido."));
        }
        
        public string Address { get; private set; }
    }
}

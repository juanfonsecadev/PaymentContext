using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Shared.Commands;
using PaymentContext.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public Email PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome inválido")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome inválido")
                .IsLowerOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome inválido")
                .IsLowerOrEqualsThan(LastName, 40, "Name.LastName", "Sobrenome inválido")
                .IsNotNullOrEmpty(TransactionCode, "TransactionCode", "Código de transação inválido")
                .IsNotNullOrEmpty(PaymentNumber, "PaymentNumber", "Número do pagamento inválido")
            );
        }

    }
}


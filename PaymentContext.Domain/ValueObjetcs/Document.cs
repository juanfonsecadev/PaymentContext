using PaymentContext.Shared.ValueObjects;
using PaymentContext.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjetcs
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento inválido")
            );
        }
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }


        private bool Validate()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)           
                return true;
            
            if (Type == EDocumentType.CPF && Number.Length == 11)
                return true;

            return false;
            
        }
    }
}
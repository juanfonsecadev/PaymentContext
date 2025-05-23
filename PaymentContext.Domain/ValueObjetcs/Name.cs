using PaymentContext.Shared.ValueObjects;
using Flunt.Validations;
using Flunt.Notifications;

namespace PaymentContext.Domain.ValueObjetcs
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
} 
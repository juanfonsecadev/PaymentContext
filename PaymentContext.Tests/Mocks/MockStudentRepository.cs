using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class MockStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {

        }

        public bool DocumentExists(string document)
        {
            if (document == "12345678909")
            {
                return true;
            }

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "hello@email.com")
            {
                return true;
            }

            return false;
        }
    }
}
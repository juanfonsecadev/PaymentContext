using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;


namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("12345678909", EDocumentType.CPF);
            _email = new Email("brucewayne@dc.com");
            _address = new Address("rua 1", "12345678", "Bairro Legal", "Gotham City", "Gotham State", "USA", "13400000");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);

        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription); //adiciona de novo a assinatura, é pra dar erro.

            Assert.IsTrue(!_student.IsValid, "Assinatura não pode ser adicionada, pois já existe uma assinatura ativa.");
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddingValidSubscription()
        {
            // Arrange
            var validPayment = new PayPalPayment(
                transactionCode: "12345678",
                paidDate: DateTime.Now,
                expireDate: DateTime.Now.AddDays(5),
                total: 10,
                totalPaid: 10,
                payer: "WAYNE CORP",
                document: _document,
                address: _address,
                email: _email
            );

            var validSubscription = new Subscription(null);
            validSubscription.AddPayment(validPayment);

            // Act
            _student.AddSubscription(validSubscription);

            // Assert
            Assert.IsTrue(_student.IsValid, "Uma assinatura com pagamento válido deveria tornar o estudante válido.");
        }

    }
}
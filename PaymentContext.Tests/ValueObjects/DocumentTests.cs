using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Domain.Enums;


namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        //Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnErrorWhenCPNJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }
        
        [TestMethod]
        public void ShouldReturnSuccessWhenCPNJIsValid()
        {
            var doc = new Document("34110468000150", EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }
        
        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("34225545806", EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}
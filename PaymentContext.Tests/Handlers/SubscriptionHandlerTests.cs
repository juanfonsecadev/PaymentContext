using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Enums;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        //Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new MockStudentRepository(), new MockEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Juan";
            command.LastName = "Fonseca";
            command.Document = "12345678909";
            command.Email = "hello2@email.com";
            command.BarCode = "123456789";
            command.BoletoNumber = "1234654987";
            command.PaymentNumber = "123123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "WAYNE CORP";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = new Email("teste@email.com");
            command.Street = "rua123";
            command.Number = "77";
            command.Neighborhood = "bairro legal";
            command.City = "belory hills";
            command.State = "general mines";
            command.Country = "guiana portuguesa";
            command.ZipCode = "31215623234";


            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }
    }

}
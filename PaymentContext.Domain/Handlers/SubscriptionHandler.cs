using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.Enums;
using Flunt.Notifications;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>, IHandler<CreateCreditCardSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {

            //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado. 
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está sendo utilizado.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está sendo utilizado.");

            //Gerar os Value Objects.
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as Entidades.
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email); 

            //Relacionar as entidades.
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações.
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações.
            _repository.CreateSubscription(student);

            //Enviar o email de boas vindas.
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo!", "Sua assinatura foi criada com sucesso!");

            //Retornar as informações.
            return new CommandResult(true, "Assinatura realizada com sucesso!");

        }


        //Método para criar a assinatura do PayPal.
        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado. 
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está sendo utilizado.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está sendo utilizado.");

            //Gerar os Value Objects.
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as Entidades.
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, email); 

            //Relacionar as entidades.
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações.
            AddNotifications(name, document, email, address, student, subscription, payment);

            //checar as notificações
            if (!IsValid)
            {
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Salvar as informações.
            _repository.CreateSubscription(student);

            //Enviar o email de boas vindas.
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo!", "Sua assinatura foi criada com sucesso!");

            //Retornar as informações.
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
             //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado. 
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está sendo utilizado.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está sendo utilizado.");

            //Gerar os Value Objects.
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as Entidades.
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName, 
                command.CardNumber, 
                command.LastTransactionNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, email); 

            //Relacionar as entidades.
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações.
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações.
            _repository.CreateSubscription(student);

            //Enviar o email de boas vindas.
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo!", "Sua assinatura foi criada com sucesso!");

            //Retornar as informações.
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;

namespace PaymentContext.Shared.ValueObjects
{
    public abstract class ValueObject : Notifiable<Notification>
    {
        protected ValueObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
    
}
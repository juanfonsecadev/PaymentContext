using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjetcs
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Street, "Address.Street", "Endereço inválido")
                .IsNotNullOrEmpty(Number, "Address.Number", "Número inválido")
                .IsNotNullOrEmpty(Neighborhood, "Address.Neighborhood", "Bairro inválido")
                .IsNotNullOrEmpty(City, "Address.City", "Cidade inválida")
                .IsNotNullOrEmpty(State, "Address.State", "Estado inválido")
                .IsNotNullOrEmpty(Country, "Address.Country", "País inválido")
                .IsNotNullOrEmpty(ZipCode, "Address.ZipCode", "CEP inválido"));
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}
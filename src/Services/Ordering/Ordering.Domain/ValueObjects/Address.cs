using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct Address
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string EmailAddress { get; init; }
        public string AddressLine { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string state, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string state, string country, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new DomainException("First name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainException("Last name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new DomainException("Email address cannot be empty.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new DomainException("Invalid email address format.");
            }
            if (string.IsNullOrWhiteSpace(addressLine))
            {
                throw new DomainException("Address line cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new DomainException("State cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new DomainException("Country cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new DomainException("Zip code cannot be empty.");
            }
            if (zipCode.Length < 3)
            {
                throw new DomainException("Zip code is too short.");
            }

            return new Address(firstName, lastName, emailAddress, addressLine, state, country, zipCode);
        }
    }
}
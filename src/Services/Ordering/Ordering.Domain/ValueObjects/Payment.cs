using Ordering.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct Payment
    {
        public string CardNumber { get; init; }
        public string CardHolderName { get; init; }
        public string CardExpiration { get; init; }
        public string CardSecurityNumber { get; init; }
        public int PaymentMethod { get; init; }

        private Payment(string cardNumber, string cardHolderName, string cardExpiration, string cardSecurityNumber, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string cardHolderName, string cardExpiration, string cardSecurityNumber, int paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new DomainException("Card number cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(cardHolderName))
            {
                throw new DomainException("Card holder name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(cardExpiration))
            {
                throw new DomainException("Card expiration cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(cardSecurityNumber))
            {
                throw new DomainException("Card security number cannot be empty.");
            }

            if (cardNumber.Length < 13 || cardNumber.Length > 19)
            {
                throw new DomainException("Invalid card number length.");
            }

            if (!Regex.IsMatch(cardExpiration, @"^(0|1)\/\d{2}$"))
            {
                throw new DomainException("Invalid card expiration format. Use MM/YY.");
            }

            return new Payment(cardNumber, cardHolderName, cardExpiration, cardSecurityNumber, paymentMethod);
        }
    }

  
}
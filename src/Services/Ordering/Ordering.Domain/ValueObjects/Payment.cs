#region

using System.Text.RegularExpressions;

#endregion

namespace Ordering.Domain.ValueObjects
{
    public readonly record struct Payment
    {
        private Payment(string cardNumber, string cardHolderName, string cardExpiration, string cardSecurityNumber,
            int paymentMethod)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            PaymentMethod = paymentMethod;
        }

        public string CardNumber { get; init; }
        public string CardHolderName { get; init; }
        public string CardExpiration { get; init; }
        public string CardSecurityNumber { get; init; }
        public int PaymentMethod { get; init; }

        public static Payment Of(string cardNumber, string cardHolderName, string cardExpiration,
            string cardSecurityNumber,
            int paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty.");

            if (string.IsNullOrWhiteSpace(cardHolderName))
                throw new ArgumentException("Card holder name cannot be empty.");

            if (string.IsNullOrWhiteSpace(cardExpiration))
                throw new ArgumentException("Card expiration date cannot be empty.");

            if (!Regex.IsMatch(cardExpiration, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                throw new ArgumentException("Invalid card expiration format. Use MM/YY.");

            if (string.IsNullOrWhiteSpace(cardSecurityNumber) || cardSecurityNumber.Length != 3)
                throw new ArgumentException("Card security number (CVV) must be 3 digits.");

            if (cardNumber.Length < 13 || cardNumber.Length > 19)
                throw new ArgumentException("Card number length is invalid.");

            return new Payment(cardNumber, cardHolderName, cardExpiration, cardSecurityNumber, paymentMethod);
        }
    }
}
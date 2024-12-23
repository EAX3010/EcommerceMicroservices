﻿namespace Ordering.Domain.Models.ValueObjects
{
    public record Address
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string City { get; init; } = default!;
        public string State { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string ZipCode { get; init; } = default!;
    }
}
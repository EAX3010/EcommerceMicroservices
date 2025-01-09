﻿using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    internal class Customer : Entity<CustomerId>
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public static Customer Create(CustomerId id,  string name, string email)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

            Customer customer = new Customer()
            { 
                Id = id, 
                Name = name,
                Email = email 
            };
            return customer;
        }
    }
}

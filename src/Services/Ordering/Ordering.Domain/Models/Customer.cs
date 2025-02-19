﻿namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public static Customer Create(CustomerId id, string name, string email)
        {
            var customer = new Customer
            {
                Id = id,
                Name = name,
                Email = email
            };
            return customer;
        }
    }
}
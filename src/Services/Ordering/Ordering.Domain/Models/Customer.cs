using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    internal class Customer : Entity<CustomerId>
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
    }
}

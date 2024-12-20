using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}

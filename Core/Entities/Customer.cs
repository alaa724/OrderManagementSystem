using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Order_Agregate;

namespace Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}

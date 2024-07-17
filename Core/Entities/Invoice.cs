using Core.Entities.Order_Agregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Invoice : BaseEntity
    {
        public int OrderId { get; set; }

        public DateTimeOffset InvoiceDate = DateTimeOffset.UtcNow;

        public decimal TotalAmount { get; set; }

        public Order Order { get; set; }
    }
}

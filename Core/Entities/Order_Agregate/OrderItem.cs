using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Order_Agregate
{
    public class OrderItem : BaseEntity
    {
        private OrderItem()
        {
        }

        public OrderItem(int quantity, decimal unitPrice, decimal discount, ProductItemOrder product)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Product = product;
        }

       
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public ProductItemOrder Product { get; set; }
    }
}

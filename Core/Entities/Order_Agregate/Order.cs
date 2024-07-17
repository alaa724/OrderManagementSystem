using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Order_Agregate
{
    public class Order : BaseEntity
    {
        private Order()
        {
        }
        public Order(int customerId, DateTimeOffset orderDate, decimal totalAmount, ICollection<OrderItem> items, string paymentMethod, Customer customer, Invoice invoice)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            Items = items;
            PaymentMethod = paymentMethod;
            Customer = customer;
            Invoice = invoice;
        }


        public int CustomerId { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public string PaymentMethod { get; set; }

        public OrderStatus Status = OrderStatus.Pending;

        public Customer Customer { get; set; }

        public Invoice Invoice { get; set; }


    }
}

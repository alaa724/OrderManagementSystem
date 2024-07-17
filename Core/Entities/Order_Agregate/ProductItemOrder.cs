using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Order_Agregate
{
    public class ProductItemOrder
    {
        // Use ParameterLess Constractor for EF Core
        private ProductItemOrder()
        {
        }

        public ProductItemOrder(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}

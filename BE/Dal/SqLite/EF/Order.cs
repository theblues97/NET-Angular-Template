using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.SqLite.EF
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ShopProductId { get; set; }
        public int Quantity { get; set; }
    }
}

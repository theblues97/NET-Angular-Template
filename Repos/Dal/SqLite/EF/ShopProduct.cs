using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.SqLite.EF
{
    public class ShopProduct
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
    }
}

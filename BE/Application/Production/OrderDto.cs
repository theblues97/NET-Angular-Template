using System.Globalization;
using System.Text.Json.Serialization;
using Core.Utilities;

namespace Application.Production
{
    public class OrderDto
    {
        public string CustomerName { get; set; } = null!;
        [JsonIgnore]
        public DateTime? CustomerDob { get; set; }
        public string CustomerEmail { get; set; } = null!;

        public string ShopName { get; set; } = null!;
        public string? ShopLocation { get; set; }

        public string ProductName { get; set; } = null!;
        public string? ProductImage { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        
        public string? CustomerDobStr 
        {
            get { return CustomerDob?.ToString(DateUtilities.DATE_FORMAT_STR); }
            set { CustomerDob = DateTime.ParseExact(value, DateUtilities.DATE_FORMAT_STR, null, DateTimeStyles.None); }
        }
    }
}

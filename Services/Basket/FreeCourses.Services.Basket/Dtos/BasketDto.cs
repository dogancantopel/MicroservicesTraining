using System.Collections.Generic;
using System.Linq;

namespace FreeCourses.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(m => m.Price * m.Quantity); }
    }
}

using ASPNETDemo3.Data;

namespace ASPNETDemo3.Models
{
    public class DataOrderTable
    {
        public OrderTable OrderTable { get; set; }
        public int[] FoodIds { get; set; }
    }
}

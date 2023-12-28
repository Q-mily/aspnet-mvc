using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETDemo3.Data
{
    public class OrderTable
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        [DisplayName("Tên loại bàn")]
        public string table_name { get; set; }

        [DisplayName("Đơn giá bàn")]
        public int? UnitPrice { get; set; }

        [DisplayName("Số lượng bàn")]
        public int? Amount { get; set; }

        [DisplayName("Tổng tiền bàn")]
        public int? TotalPrice {  get; set; }
        public int status { get; set; }
        public DateTime createdAt { get; set; }

        public List<Food>? Foods { get; set; } = new();
    }
}

using System.ComponentModel;

namespace ASPNETDemo3.Data
{
    public class Food
    {
        public int Id { get; set; }
        [DisplayName("Tên món ăn")]
        public string Name { get; set; }
        [DisplayName("Đơn giá")]
        public int Price { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public int status { get; set; }
        public DateTime createdAt { get; set; }
        public List<OrderTable>? OrderTables { get; } = new();
    }
}

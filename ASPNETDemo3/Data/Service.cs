using System.ComponentModel;

namespace ASPNETDemo3.Data
{
    public class Service
    {
        public int Id { get; set; }

        [DisplayName("Tên dịch vụ")]
        public string Name { get; set; }

        [DisplayName("Đơn giá")]
        public int Price { get; set; }
        public string? Description { get; set; }     
        public DateTime? CreatedAt { get; set; }
    }
}

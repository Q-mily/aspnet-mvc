using System.ComponentModel;

namespace ASPNETDemo3.Data
{
    public class Lobby
    {
        public int Id { get; set; }

        [DisplayName("Tên Sảnh")]
        public string Name { get; set; }
        [DisplayName("Số lượng bàn tối thiểu")]
        public int MinCountTable { get; set; }
        public string? ImagePath { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        public int status { get; set; }
        public DateTime createdAt {  get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

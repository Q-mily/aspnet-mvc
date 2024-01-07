using System.ComponentModel;

namespace ASPNETDemo3.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int? lobbyId { get; set; }

        [DisplayName("Tên khách đặt")]
        public string customerName {  get; set; }

        [DisplayName("Số điện thoại")]
        public string? customerPhone { get; set; }

        [DisplayName("Tên chú rể")]
        public string groomName { get; set; }

        [DisplayName("Tên cô dâu")]
        public string brideName { get; set; }
        public string? ca {  get; set; }
        [DisplayName("Ngày tổ chức")]
        public DateTime dateAt { get; set; }
        [DisplayName("Trạng thái")]
        public int? status { get; set; }
        public DateTime? createdAt { get; set; }
        [DisplayName("Sảnh")]
        public Lobby? Lobby { get; set; }
        public List<OrderTable>? OrderTables { get; set; }

        [DisplayName("Tổng tiền")]
        public int? TotalPrice { get; set; }
        [DisplayName("Ngày thanh toán")]
        public DateTime? PaymentAt { get; set; }
    }
}

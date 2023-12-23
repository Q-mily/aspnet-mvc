namespace ASPNETDemo3.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int? lobbyId { get; set; }
        public string? customerName {  get; set; }
        public string? customerPhone {  get; set; }
        public string? groomName { get; set; }
        public string? brideName { get; set; }
        public string? ca {  get; set; }
        public DateTime? dateAt { get; set; }
        public int? status { get; set; }
        public DateTime? createdAt { get; set; }
        public Lobby Lobby { get; set; }
    }
}

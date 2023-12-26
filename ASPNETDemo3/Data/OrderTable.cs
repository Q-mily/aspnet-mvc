namespace ASPNETDemo3.Data
{
    public class OrderTable
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public string table_name { get; set; }
        public int? UnitPrice { get; set; }
        public int? Amount { get; set; }
        public int? TotalPrice {  get; set; }
        public int status { get; set; }
        public DateTime createdAt { get; set; }

        public List<Food>? Foods { get; set; } = new();
    }
}

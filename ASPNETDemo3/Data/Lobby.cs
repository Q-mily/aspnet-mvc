namespace ASPNETDemo3.Data
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinCountTable { get; set; }
        public string ImagePath { get; set; }
        public string? Description { get; set; }

        public int status { get; set; }
        public DateTime createdAt {  get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

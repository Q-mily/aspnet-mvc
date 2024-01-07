namespace ASPNETDemo3.Models
{
    public class OrderFilterModel
    {
        public string? search { get; set; }
        public int? status { get; set; }
        public int? lobbyId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set;}

        public int Page { get; set;}
        public int? TotalCount { get; set; }
        public int PageSize { get; set;}
    }
}
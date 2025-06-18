namespace CrudApp2.Models
{
    public class OrderSearchModel
    {
        public string? UserName { get; set; }
        public string? CarBrand { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<Order> Results { get; set; } = new List<Order>();
    }
}

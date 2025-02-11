namespace GeekGardenCase3.Models
{
    public class Kpi
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public decimal Score { get; set; }
        public DateTime Date { get; set; }
    }
}

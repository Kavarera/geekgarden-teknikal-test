namespace GeekGardenCase3.Models
{
    public class Company
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Kpi> Kpis { get; set; }
    }
}

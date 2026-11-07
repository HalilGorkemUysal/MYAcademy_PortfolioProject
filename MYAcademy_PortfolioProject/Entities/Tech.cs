namespace MYAcademy_PortfolioProject.Entities
{
    public class Tech
    {
        public int TechId { get; set; }
        public string TechName{ get; set; }
        public int ProjectId{ get; set; }
        public Project Project{ get; set; }
    }
}

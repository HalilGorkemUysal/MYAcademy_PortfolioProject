using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.ViewModels
{
    public class DashboardViewModel
    {
        // İstatistikler
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }

        // Education tablosu henüz yok
        public int? EducationCount { get; set; }

        public int TotalMessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int ReadMessageCount { get; set; }

        // Son 5 mesaj
        public List<Message> LastMessages { get; set; } = new();

        // Son 3 proje
        public List<Project> LastProjects { get; set; } = new();

        // Kategori özeti
        public List<CategoryProjectCount> CategorySummary { get; set; } = new();

        // Son 7 gün mesaj istatistikleri
        public List<DailyMessageCount> WeeklyMessages { get; set; } = new();
    }

    public class CategoryProjectCount
    {
        public string CategoryName { get; set; } = "";
        public int ProjectCount { get; set; }
    }

    public class DailyMessageCount
    {
        public DateTime Date { get; set; }
        public int MessageCount { get; set; }
    }
}

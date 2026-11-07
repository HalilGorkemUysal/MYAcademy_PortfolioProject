
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.ViewModels;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();

            // PROJE İSTATİSTİKLERİ
            model.TotalProjectCount = _context.Projects.Count();

            model.ActiveProjectCount = _context.Projects
                .Count(x => x.IsActive);

            // YETENEK VE DENEYİM
            model.SkillCount = _context.Skills.Count();

            model.ExperienceCount = _context.Experiences.Count();

            // Eğitim verisi henüz ayrı tabloda tutulmuyor
            model.EducationCount = null;

            // MESAJ İSTATİSTİKLERİ
            model.TotalMessageCount = _context.Messages.Count();

            model.UnreadMessageCount = _context.Messages
                .Count(x => !x.IsRead);

            model.ReadMessageCount = _context.Messages
                .Count(x => x.IsRead);

            // SON 5 MESAJ
            model.LastMessages = _context.Messages
                .OrderByDescending(x => x.SendDate)
                .ThenByDescending(x => x.MessageId)
                .Take(5)
                .ToList();

            // SON 3 PROJE
            model.LastProjects = _context.Projects
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedDate)
                .ThenByDescending(x => x.ProjectId)
                .Take(3)
                .ToList();

            // KATEGORİLERE GÖRE PROJE SAYISI
            model.CategorySummary = _context.Categories
                .Select(x => new CategoryProjectCount
                {
                    CategoryName = x.CategoryName,
                    ProjectCount = _context.Projects
                        .Count(p => p.CategoryId == x.CategoryId)
                })
                .ToList();

            // SON 7 GÜN MESAJ SAYISI
            var today = DateTime.Today;
            var startDate = today.AddDays(-6);
            var endDate = today.AddDays(1);

            var recentMessages = _context.Messages
                .Where(x => x.SendDate >= startDate &&
                            x.SendDate < endDate)
                .Select(x => x.SendDate)
                .ToList();

            model.WeeklyMessages = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    var day = startDate.AddDays(i);

                    return new DailyMessageCount
                    {
                        Date = day,
                        MessageCount = recentMessages
                            .Count(x => x.Date == day)
                    };
                })
                .ToList();

            return View(model);
        }
    }
}

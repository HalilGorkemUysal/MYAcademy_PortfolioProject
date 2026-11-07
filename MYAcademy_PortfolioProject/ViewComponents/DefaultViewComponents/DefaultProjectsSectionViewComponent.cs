using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultProjectsSectionViewComponent : ViewComponent
    {
        private readonly PortfolioContext _context;

        public DefaultProjectsSectionViewComponent(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .Include(x => x.Teches)
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            return View(values);
        }
    }
}

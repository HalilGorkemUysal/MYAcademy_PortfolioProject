
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultAboutmeSectionViewComponent : ViewComponent
    {
        private readonly PortfolioContext _context;

        public DefaultAboutmeSectionViewComponent(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Abouts
                .OrderByDescending(x => x.AboutId)
                .FirstOrDefault();

            return View(value);
        }
    }
}

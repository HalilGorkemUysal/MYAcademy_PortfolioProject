using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultAboutmeSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

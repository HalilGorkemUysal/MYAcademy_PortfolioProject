using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultPhilosophySectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

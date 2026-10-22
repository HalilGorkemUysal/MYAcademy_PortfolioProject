using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultSkillsSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class DefaultContactSectionViewComponent : ViewComponent
    {
        private readonly PortfolioContext _context;

        public DefaultContactSectionViewComponent(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Contacts.FirstOrDefault();
            ViewBag.title = values.Title;
            ViewBag.email = values.Email;
            ViewBag.location = values.Location;
            ViewBag.description = values.Description;
            ViewBag.phonenumber = values.PhoneNumber;

            return View();
        }
    }
}

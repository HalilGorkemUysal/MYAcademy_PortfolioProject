
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroTitleController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroTitleController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult HeroTitleList()
        {
            var values = _context.HeroTitles
                .OrderBy(x => x.Order)
                .ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateHeroTitle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHeroTitle(HeroTitle heroTitle)
        {
            _context.HeroTitles.Add(heroTitle);
            _context.SaveChanges();

            return RedirectToAction("HeroTitleList");
        }

        public IActionResult DeleteHeroTitle(int id)
        {
            var value = _context.HeroTitles.Find(id);

            if (value == null)
                return NotFound();

            _context.HeroTitles.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("HeroTitleList");
        }

        [HttpGet]
        public IActionResult UpdateHeroTitle(int id)
        {
            var value = _context.HeroTitles.Find(id);

            if (value == null)
                return NotFound();

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateHeroTitle(HeroTitle heroTitle)
        {
            var value = _context.HeroTitles.Find(heroTitle.HeroTitleId);

            if (value == null)
                return NotFound();

            value.Title = heroTitle.Title;
            value.Order = heroTitle.Order;
            value.IsActive = heroTitle.IsActive;

            _context.SaveChanges();

            return RedirectToAction("HeroTitleList");
        }
    }
}

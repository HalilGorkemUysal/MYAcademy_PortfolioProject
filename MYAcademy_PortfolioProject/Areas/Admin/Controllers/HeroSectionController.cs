
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroSectionController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult HeroSectionList()
        {
            var values = _context.HeroSections.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateHeroSection()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHeroSection(HeroSection heroSection)
        {
            _context.HeroSections.Add(heroSection);
            _context.SaveChanges();

            return RedirectToAction("HeroSectionList");
        }

        public IActionResult DeleteHeroSection(int id)
        {
            var value = _context.HeroSections.Find(id);

            if (value == null)
                return NotFound();

            _context.HeroSections.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("HeroSectionList");
        }

        [HttpGet]
        public IActionResult UpdateHeroSection(int id)
        {
            var value = _context.HeroSections.Find(id);

            if (value == null)
                return NotFound();

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateHeroSection(HeroSection heroSection)
        {
            var value = _context.HeroSections.Find(heroSection.HeroSectionId);

            if (value == null)
                return NotFound();

            value.Greeting = heroSection.Greeting;
            value.Title = heroSection.Title;
            value.Description = heroSection.Description;
            value.MainImageUrl = heroSection.MainImageUrl;

            _context.SaveChanges();

            return RedirectToAction("HeroSectionList");
        }
    }
}

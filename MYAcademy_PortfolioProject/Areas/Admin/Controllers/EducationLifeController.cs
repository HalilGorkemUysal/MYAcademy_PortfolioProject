
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationLifeController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationLifeController(PortfolioContext context)
        {
            _context = context;
        }

        // EĞİTİMLERİ LİSTELE
        public IActionResult EducationLifeList()
        {
            var values = _context.EducationLives.ToList();
            return View(values);
        }

        // EĞİTİM EKLEME SAYFASI
        [HttpGet]
        public IActionResult CreateEducationLife()
        {
            return View();
        }

        // EĞİTİM EKLEME İŞLEMİ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEducationLife(EducationLife educationLife)
        {
            if (!ModelState.IsValid)
                return View(educationLife);

            _context.EducationLives.Add(educationLife);
            _context.SaveChanges();

            return RedirectToAction("EducationLifeList");
        }

        // EĞİTİM SİLME
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEducationLife(int id)
        {
            var value = _context.EducationLives.Find(id);

            if (value == null)
                return NotFound();

            _context.EducationLives.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("EducationLifeList");
        }

        // EĞİTİM GÜNCELLEME SAYFASI
        [HttpGet]
        public IActionResult UpdateEducationLife(int id)
        {
            var value = _context.EducationLives.Find(id);

            if (value == null)
                return NotFound();

            return View(value);
        }

        // EĞİTİM GÜNCELLEME İŞLEMİ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEducationLife(EducationLife educationLife)
        {
            if (!ModelState.IsValid)
                return View(educationLife);

            var value = _context.EducationLives.Find(
                educationLife.EducationLifeId);

            if (value == null)
                return NotFound();

            value.Header = educationLife.Header;
            value.Title = educationLife.Title;
            value.Description = educationLife.Description;
            value.Tag = educationLife.Tag;

            _context.SaveChanges();

            return RedirectToAction("EducationLifeList");
        }
    }
}

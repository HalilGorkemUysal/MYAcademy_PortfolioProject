
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TechController : Controller
    {
        private readonly PortfolioContext _context;

        public TechController(PortfolioContext context)
        {
            _context = context;
        }

        // TEKNOLOJİLERİ LİSTELE
        public IActionResult TechList()
        {
            var values = _context.Teches
                .Include(x => x.Project)
                .ToList();

            return View(values);
        }

        // EKLEME SAYFASI
        [HttpGet]
        public IActionResult CreateTech()
        {
            var values = _context.Projects.Select(x => new SelectListItem()
            {
                Value = x.ProjectId.ToString(),
                Text = x.ProjectTitle
            }).ToList();

            ViewBag.v1 = values;

            return View();
        }

        // TEKNOLOJİ EKLE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTech(Tech tech)
        {
            if (string.IsNullOrWhiteSpace(tech.TechName) ||
                !_context.Projects.Any(x => x.ProjectId == tech.ProjectId))
            {
                ViewBag.Error = "Teknoloji adı ve geçerli bir proje seçimi gereklidir.";

                ViewBag.v1 = _context.Projects.Select(x => new SelectListItem
                {
                    Value = x.ProjectId.ToString(),
                    Text = x.ProjectTitle
                }).ToList();

                return View(tech);
            }

            var newTech = new Tech
            {
                TechName = tech.TechName.Trim(),
                ProjectId = tech.ProjectId
            };

            _context.Teches.Add(newTech);
            _context.SaveChanges();

            return RedirectToAction("TechList");
        }

        // TEKNOLOJİ SİL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTech(int id)
        {
            var value = _context.Teches.Find(id);

            if (value == null)
                return NotFound();

            _context.Teches.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("TechList");
        }

        // GÜNCELLEME SAYFASI
        [HttpGet]
        public IActionResult UpdateTech(int id)
        {
            var tech = _context.Teches.Find(id);

            if (tech == null)
                return NotFound();

            var values = _context.Projects.Select(x => new SelectListItem()
            {
                Value = x.ProjectId.ToString(),
                Text = x.ProjectTitle
            }).ToList();

            ViewBag.v1 = values;

            return View(tech);
        }

        // TEKNOLOJİ GÜNCELLE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTech(Tech tech)
        {
            var existingTech = _context.Teches.Find(tech.TechId);

            if (existingTech == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(tech.TechName) ||
                !_context.Projects.Any(x => x.ProjectId == tech.ProjectId))
            {
                ViewBag.Error = "Teknoloji adı ve geçerli bir proje seçimi gereklidir.";

                ViewBag.v1 = _context.Projects.Select(x => new SelectListItem
                {
                    Value = x.ProjectId.ToString(),
                    Text = x.ProjectTitle
                }).ToList();

                return View(tech);
            }

            existingTech.TechName = tech.TechName.Trim();
            existingTech.ProjectId = tech.ProjectId;

            _context.SaveChanges();

            return RedirectToAction("TechList");
        }
    }
}

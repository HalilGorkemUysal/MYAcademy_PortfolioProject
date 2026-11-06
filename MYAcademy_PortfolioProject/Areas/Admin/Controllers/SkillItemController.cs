
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillItemController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillItemController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillItemList()
        {
            var values = _context.SkillItems
                .Include(x => x.Skill)
                .ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkillItem()
        {
            var values = _context.Skills.Select(x => new SelectListItem()
            {
                Value = x.SkillId.ToString(),
                Text = x.Title
            }).ToList();

            ViewBag.v1 = values;

            return View();
        }

        [HttpPost]
        public IActionResult CreateSkillItem(SkillItem skillItem)
        {
            _context.SkillItems.Add(skillItem);
            _context.SaveChanges();

            return RedirectToAction("SkillItemList");
        }

        public IActionResult DeleteSkillItem(int id)
        {
            var value = _context.SkillItems.Find(id);

            if (value == null)
                return NotFound();

            _context.SkillItems.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("SkillItemList");
        }

        [HttpGet]
        public IActionResult UpdateSkillItem(int id)
        {
            var value = _context.SkillItems.Find(id);

            if (value == null)
                return NotFound();

            var values = _context.Skills.Select(x => new SelectListItem()
            {
                Value = x.SkillId.ToString(),
                Text = x.Title
            }).ToList();

            ViewBag.v1 = values;

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSkillItem(SkillItem skillItem)
        {
            var value = _context.SkillItems.Find(skillItem.SkillItemId);

            if (value == null)
                return NotFound();

            value.SkillItemTitle = skillItem.SkillItemTitle;
            value.SkillId = skillItem.SkillId;

            _context.SaveChanges();

            return RedirectToAction("SkillItemList");
        }
    }
}

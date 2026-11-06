using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;
        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
                return NotFound();

            _context.Categories.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
                return NotFound();

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            var value = _context.Categories.Find(category.CategoryId);
            if (value == null)
                return NotFound();

            value.CategoryName = category.CategoryName;
            _context.SaveChanges();

            return RedirectToAction("CategoryList");
        }
    }
}

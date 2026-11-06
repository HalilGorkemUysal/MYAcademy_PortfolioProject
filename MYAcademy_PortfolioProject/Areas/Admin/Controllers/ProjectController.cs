using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using System.Security.Permissions;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList()
        {
            var values = _context.Projects.Include(x => x.Category).ToList();
            return View(values);
        }

        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null)
                return NotFound();

            _context.Projects.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            var values = _context.Categories.Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.v1 = values;

            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var project = _context.Projects.Find(id);

            var values = _context.Categories.Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.v1 = values;

            return View(project);
        }

        [HttpPost]
        public IActionResult UpdateProject(Project project)
        {

            _context.Projects.Update(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
    }
}

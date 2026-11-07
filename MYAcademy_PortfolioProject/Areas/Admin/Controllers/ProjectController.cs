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
        private readonly IWebHostEnvironment _env;

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var folderPath = Path.Combine(
                    _env.WebRootPath, "images", "projects");

                Directory.CreateDirectory(folderPath);

                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

                if (!allowedExtensions.Contains(extension) ||
                    !imageFile.ContentType.StartsWith("image/") ||
                    imageFile.Length > 7 * 1024 * 1024)
                {
                    return BadRequest("Geçersiz görsel formatı veya dosya boyutu. En fazla 5 MB.");
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                project.ImageUrl = "/images/projects/" + fileName;
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

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
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            var existingProject = await _context.Projects
                .FindAsync(project.ProjectId);

            if (existingProject == null)
                return NotFound();

            existingProject.ProjectTitle = project.ProjectTitle;
            existingProject.Description = project.Description;
            existingProject.CategoryId = project.CategoryId;
            existingProject.GithubUrl = project.GithubUrl;
            existingProject.IsActive = project.IsActive;

            if (imageFile != null && imageFile.Length > 0)
            {
                var folderPath = Path.Combine(
                    _env.WebRootPath, "images", "projects");

                Directory.CreateDirectory(folderPath);

                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

                if (!allowedExtensions.Contains(extension) ||
                    !imageFile.ContentType.StartsWith("image/") ||
                    imageFile.Length > 7 * 1024 * 1024)
                {
                    return BadRequest("Geçersiz görsel formatı veya dosya boyutu. En fazla 5 MB.");
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingProject.ImageUrl = "/images/projects/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ProjectList");
        }
    }
}
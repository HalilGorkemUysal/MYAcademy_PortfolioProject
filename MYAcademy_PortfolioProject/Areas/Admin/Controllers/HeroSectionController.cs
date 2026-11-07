
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;


        public HeroSectionController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeroSection(
            HeroSection heroSection, IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Lütfen bir görsel seçiniz.");

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

            if (!allowedExtensions.Contains(extension) ||
                !imageFile.ContentType.StartsWith("image/") ||
                imageFile.Length > 7 * 1024 * 1024)
            {
                return BadRequest("Geçersiz görsel formatı veya boyutu. En fazla 5 MB.");
            }

            var folderPath = Path.Combine(
                _env.WebRootPath, "images", "herosections");

            Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await imageFile.CopyToAsync(stream);
            }

            heroSection.MainImageUrl = "/images/herosections/" + fileName;

            _context.HeroSections.Add(heroSection);
            await _context.SaveChangesAsync();

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateHeroSection(
            HeroSection heroSection, IFormFile? imageFile)
        {
            var value = await _context.HeroSections
                .FindAsync(heroSection.HeroSectionId);

            if (value == null)
                return NotFound();

            // Önce isteğe bağlı yeni görseli kontrol et
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

                if (!allowedExtensions.Contains(extension) ||
                    !imageFile.ContentType.StartsWith("image/") ||
                    imageFile.Length > 7 * 1024 * 1024)
                {
                    return BadRequest("Geçersiz görsel formatı veya boyutu. En fazla 5 MB.");
                }

                var folderPath = Path.Combine(
                    _env.WebRootPath, "images", "herosections");

                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                value.MainImageUrl = "/images/herosections/" + fileName;
            }

            value.Greeting = heroSection.Greeting;
            value.Title = heroSection.Title;
            value.Description = heroSection.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("HeroSectionList");
        }

    }
}
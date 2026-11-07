
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(
            PortfolioContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // HAKKIMDA LİSTESİ
        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        // HAKKIMDA EKLEME SAYFASI
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        // HAKKIMDA EKLEME İŞLEMİ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbout(
            About about, IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError(
                    "ImageUrl", "Lütfen bir profil görseli seçiniz.");

                return View(about);
            }

            // Dosya kontrolü
            var extension = Path.GetExtension(
                imageFile.FileName).ToLowerInvariant();

            var allowedExtensions = new[]
            {
                ".jpg", ".jpeg", ".png", ".webp"
            };

            if (!allowedExtensions.Contains(extension) ||
                !imageFile.ContentType.StartsWith("image/") ||
                imageFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(
                    "ImageUrl",
                    "Geçerli bir görsel seçiniz. Maksimum 5 MB.");

                return View(about);
            }

            // ImageUrl formdan gelmeyecek, dosya yüklenince oluşacak.
            ModelState.Remove(nameof(About.ImageUrl));

            if (!ModelState.IsValid)
                return View(about);

            // Görseli kaydet
            var folderPath = Path.Combine(
                _env.WebRootPath, "images", "about");

            Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(
                filePath, FileMode.CreateNew))
            {
                await imageFile.CopyToAsync(stream);
            }

            about.ImageUrl = "/images/about/" + fileName;

            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();

            return RedirectToAction("AboutList");
        }

        // HAKKIMDA SİLME
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);

            if (value == null)
                return NotFound();

            _context.Abouts.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("AboutList");
        }

        // HAKKIMDA GÜNCELLEME SAYFASI
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var value = _context.Abouts.Find(id);

            if (value == null)
                return NotFound();

            return View(value);
        }

        // HAKKIMDA GÜNCELLEME İŞLEMİ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbout(
            About about, IFormFile? imageFile)
        {
            var value = await _context.Abouts
                .FindAsync(about.AboutId);

            if (value == null)
                return NotFound();

            // ImageUrl formdan gelmediği için doğrulamadan çıkar.
            ModelState.Remove(nameof(About.ImageUrl));

            if (!ModelState.IsValid)
            {
                // Hata durumunda eski görselin gösterilmesi için
                about.ImageUrl = value.ImageUrl;
                return View(about);
            }

            // Yeni görsel seçildiyse güncelle
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(
                    imageFile.FileName).ToLowerInvariant();

                var allowedExtensions = new[]
                {
                    ".jpg", ".jpeg", ".png", ".webp"
                };

                if (!allowedExtensions.Contains(extension) ||
                    !imageFile.ContentType.StartsWith("image/") ||
                    imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError(
                        "ImageUrl",
                        "Geçerli bir görsel seçiniz. Maksimum 5 MB.");

                    about.ImageUrl = value.ImageUrl;
                    return View(about);
                }

                var folderPath = Path.Combine(
                    _env.WebRootPath, "images", "about");

                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(
                    filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                value.ImageUrl = "/images/about/" + fileName;
            }

            // Diğer alanları güncelle
            value.Title = about.Title;
            value.Description = about.Description;
            value.ExperienceCount = about.ExperienceCount;
            value.EducationContentCount = about.EducationContentCount;
            value.StudentCount = about.StudentCount;

            await _context.SaveChangesAsync();

            return RedirectToAction("AboutList");
        }
    }
}

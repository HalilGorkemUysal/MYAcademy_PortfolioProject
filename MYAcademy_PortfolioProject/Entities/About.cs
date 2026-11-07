
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(200, MinimumLength = 5,
            ErrorMessage = "Başlık 5 ile 200 karakter arasında olmalıdır.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [StringLength(2000, MinimumLength = 20,
            ErrorMessage = "Açıklama 20 ile 2000 karakter arasında olmalıdır.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profil görseli zorunludur.")]
        [StringLength(500, ErrorMessage = "Görsel yolu en fazla 500 karakter olabilir.")]
        [Display(Name = "Profil Görseli")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Deneyim bilgisi zorunludur.")]
        [StringLength(20)]
        [Display(Name = "Yıl Deneyim")]
        public string ExperienceCount { get; set; } = string.Empty;

        [Required(ErrorMessage = "Eğitim içeriği sayısı zorunludur.")]
        [StringLength(20)]
        [Display(Name = "Eğitim İçeriği")]
        public string EducationContentCount { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci sayısı zorunludur.")]
        [StringLength(20)]
        [Display(Name = "Öğrenci Sayısı")]
        public string StudentCount { get; set; } = string.Empty;
    }
}

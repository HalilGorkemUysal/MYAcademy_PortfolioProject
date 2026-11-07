
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class EducationLife
    {
        [Key]
        public int EducationLifeId { get; set; }

        [Required(ErrorMessage = "Header alanı boş bırakılamaz.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Header 3 ile 100 karakter arasında olmalıdır.")]
        [Display(Name = "Header")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Eğitim başlığı boş bırakılamaz.")]
        [StringLength(200, MinimumLength = 5,
            ErrorMessage = "Eğitim başlığı 5 ile 200 karakter arasında olmalıdır.")]
        [Display(Name = "Eğitim Başlığı")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Eğitim açıklaması boş bırakılamaz.")]
        [StringLength(1000, MinimumLength = 10,
            ErrorMessage = "Açıklama 10 ile 1000 karakter arasında olmalıdır.")]
        [Display(Name = "Eğitim Açıklaması")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Etiket alanı boş bırakılamaz.")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Etiket 2 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Etiket")]
        public string Tag { get; set; }
    }
}

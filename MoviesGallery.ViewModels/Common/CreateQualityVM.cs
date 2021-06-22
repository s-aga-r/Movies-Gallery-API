using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels.Common
{
    public class CreateQualityVM
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null;


        [MinLength(10)]
        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

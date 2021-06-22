using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels.Common
{
    public class CreateDownloadLinkVM
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; } = null;


        [MinLength(10)]
        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Required]
        [Display(Name = "Link")]
        [DataType(DataType.Url)]
        public string Link { get; set; } = null;


        [Required]
        [Display(Name = "Movie")]
        public Guid MovieId { get; set; }


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

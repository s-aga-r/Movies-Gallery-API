using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesGallery.Models
{
    [DisplayColumn("Link")]
    public class DownloadLink
    {
        [Key]
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


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


        [Display(Name = "Link")]
        [DataType(DataType.Url)]
        public string Link { get; set; } = null;


        // (Movie) 1 : M (DownloadLink)
        [Display(Name = "Movie")]
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

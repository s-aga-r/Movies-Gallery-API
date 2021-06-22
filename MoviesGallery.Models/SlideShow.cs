using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesGallery.Models
{
    [DisplayColumn("Image")]
    public class SlideShow
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


        [MinLength(5)]
        [Display(Name = "Image")]
        public string Image { get; set; } = null;


        [MinLength(10)]
        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; } = DateTime.Now;


        [Display(Name = "Last Updated On")]
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;


        [Display(Name = "Order")]
        public int Order { get; set; }


        // (Movie) 1 : 1 (SlideShow)
        [Display(Name = "Movie")]
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

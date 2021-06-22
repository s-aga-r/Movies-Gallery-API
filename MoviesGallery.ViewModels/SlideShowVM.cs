using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels
{
    [DisplayColumn("Image")]
    public class SlideShowVM
    {
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }


        [Display(Name = "Title")]
        public string Title { get; set; } = null;


        [Display(Name = "Image")]
        public string Image { get; set; } = null;


        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; } = DateTime.Now;


        [Display(Name = "Last Updated On")]
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;


        [Display(Name = "Order")]
        public int Order { get; set; }


        [Display(Name = "Movie")]
        public MovieVM Movie { get; set; }
        public Guid MovieId { get; set; }


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

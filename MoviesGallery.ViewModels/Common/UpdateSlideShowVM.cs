using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels.Common
{
    public class UpdateSlideShowVM
    {
        [Required]
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }


        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; } = null;


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


        [Display(Name = "Order")]
        public int? Order { get; set; }


        [Display(Name = "Movie")]
        public Guid MovieId { get; set; }


        [Display(Name = "Active Status")]
        public bool? IsActive { get; set; } = true;
    }
}

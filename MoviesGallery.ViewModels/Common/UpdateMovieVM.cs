using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels.Common
{
    public class UpdateMovieVM
    {
        [Required]
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }


        [MinLength(2)]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "title")]
        public string Title { get; set; } = null;


        [MinLength(10)]
        [Display(Name = "story line")]
        [DataType(DataType.MultilineText)]
        public string StoryLine { get; set; } = null;


        [MinLength(100)]
        [Display(Name = "description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [DataType(DataType.Upload)]
        [Display(Name = "Cover Image")]
        public IFormFile CoverImage { get; set; } = null;


        [Range(0, 100)]
        [Display(Name = "Size in GB")]
        public float? Size { get; set; } = 0;


        [DataType(DataType.Url)]
        [Display(Name = "Sample Download Link")]
        public string SampleDownloadLink { get; set; } = null;


        [Display(Name = "Date of Release")]
        public DateTime? DateOfRelease { get; set; } = null;


        [Range(0, 10)]
        [Display(Name = "IMDB Ratings")]
        public float? IMDB { get; set; } = 0;


        [DataType(DataType.Url)]
        [Display(Name = "Trailer")]
        public string Trailer { get; set; } = null;


        [Display(Name = "Country")]
        public Guid CountryId { get; set; }


        [Display(Name = "Director")]
        public Guid DirectorId { get; set; }


        [Display(Name = "Quality")]
        public Guid QualityId { get; set; }


        [Display(Name = "Languages")]
        public List<Guid> LanguagesId { get; set; }


        [Display(Name = "FilmStars")]
        public List<Guid> FilmStarsId { get; set; }


        [Display(Name = "Categories")]
        public List<Guid> CategoriesId { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Screenshots")]
        public List<IFormFile> Screenshots { get; set; }


        [Display(Name = "Download Links")]
        public List<string> DownloadLinks { get; set; }


        [Display(Name = "Active Status")]
        public bool? IsActive { get; set; } = true;
    }
}

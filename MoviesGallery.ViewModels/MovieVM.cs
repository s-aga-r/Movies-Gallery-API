using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels
{
    [DisplayColumn("Title")]
    public class MovieVM
    {
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }


        [Display(Name = "Title")]
        public string Title { get; set; } = null;


        [Display(Name = "Story Line")]
        [DataType(DataType.MultilineText)]
        public string StoryLine { get; set; } = null;


        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Display(Name = "Cover Image")]
        public string CoverImage { get; set; } = null;


        [Display(Name = "Size in GB")]
        public float Size { get; set; } = 0;


        [Display(Name = "Total Downloads")]
        public long TotalDownloads { get; set; } = 0;


        [DataType(DataType.Url)]
        [Display(Name = "Sample Download Link")]
        public string SampleDownloadLink { get; set; } = null;


        [Display(Name = "Date of Release")]
        public DateTime? DateOfRelease { get; set; } = null;


        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; } = DateTime.Now;


        [Display(Name = "Last Updated On")]
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;


        [Display(Name = "IMDB Ratings")]
        public float IMDB { get; set; } = 0;


        [DataType(DataType.Url)]
        [Display(Name = "Trailer")]
        public string Trailer { get; set; } = null;


        [Display(Name = "Country")]
        public CountryVM Country { get; set; }


        [Display(Name = "Director")]
        public DirectorVM Director { get; set; }


        [Display(Name = "Quality")]
        public QualityVM Quality { get; set; }


        [Display(Name = "Languages")]
        public virtual List<LanguageVM> Languages { get; set; }


        [Display(Name = "Film Stars")]
        public virtual List<FilmStarVM> FilmStars { get; set; }


        [Display(Name = "Categories")]
        public virtual List<CategoryVM> Categories { get; set; }


        [Display(Name = "Screenshots")]
        public List<ScreenshotVM> Screenshots { get; set; }


        [Display(Name = "Download Links")]
        public List<DownloadLinkVM> DownloadLinks { get; set; } 


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

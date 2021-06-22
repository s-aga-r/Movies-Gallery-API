using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesGallery.Models
{
    [DisplayColumn("Title")]
    public class Movie
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
        [Display(Name = "Story Line")]
        [DataType(DataType.MultilineText)]
        public string StoryLine { get; set; } = null;


        [MinLength(100)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [MinLength(5)]
        [Display(Name = "Cover Image")]
        public string CoverImage { get; set; } = null;


        [Range(0.01, 100)]
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


        [Range(0.1, 10)]
        [Display(Name = "IMDB Ratings")]
        public float IMDB { get; set; } = 0;


        [DataType(DataType.Url)]
        [Display(Name = "Trailer")]
        public string Trailer { get; set; } = null;


        // (Country) 1 : M (Movie)
        [Display(Name = "Country")]
        public Country Country { get; set; }


        // (Director) 1 : M (Movie)
        [Display(Name = "Director")]
        public Director Director { get; set; }


        // (Quality) 1 : M (Movie)
        [Display(Name = "Quality")]
        public Quality Quality { get; set; }


        // (Language) M : M (Movie)
        [Display(Name = "Languages")]
        public virtual List<Language> Languages { get; set; }


        // (FilmStar) M : M (Movie)
        [Display(Name = "Film Stars")]
        public virtual List<FilmStar> FilmStars { get; set; }


        // (Category) M : M (Movie)
        [Display(Name = "Categories")]
        public virtual List<Category> Categories { get; set; }


        // (Screenshot) M : 1 (Movie)
        [Display(Name = "Screenshots")]
        public List<Screenshot> Screenshots { get; set; }


        // (DownloadLink) M : 1 (Movie)
        [Display(Name = "Download Links")]
        public List<DownloadLink> DownloadLinks { get; set; } 


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesGallery.ViewModels
{
    [DisplayColumn("Name")]
    public class CountryVM
    {
        [Display(Name = "Id")]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }


        [Display(Name = "Name")]
        public string Name { get; set; } = null;


        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;


        [Display(Name = "Movies")]
        public List<MovieVM> Movies { get; set; } = null;


        [Display(Name = "Active Status")]
        public bool IsActive { get; set; } = true;
    }
}

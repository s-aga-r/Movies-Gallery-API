using AutoMapper;
using MoviesGallery.Models;
using MoviesGallery.ViewModels;
using MoviesGallery.ViewModels.Common;

namespace MoviesGallery.Server.Services.AutoMapper
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Create
            CreateMap<Category, CreateCategoryVM>().ReverseMap();
            CreateMap<Country, CreateCountryVM>().ReverseMap();
            CreateMap<Director, CreateDirectorVM>().ReverseMap();
            CreateMap<DownloadLink, CreateDownloadLinkVM>().ReverseMap();
            CreateMap<FilmStar, CreateFilmStarVM>().ReverseMap();
            CreateMap<Language, CreateLanguageVM>().ReverseMap();
            CreateMap<Movie, CreateMovieVM>().ReverseMap()
                .ForMember(d => d.CoverImage, o => o.Ignore())
                .ForMember(d => d.Country, o => o.Ignore())
                .ForMember(d => d.Director, o => o.Ignore())
                .ForMember(d => d.Quality, o => o.Ignore())
                .ForMember(d => d.Languages, o => o.Ignore())
                .ForMember(d => d.FilmStars, o => o.Ignore())
                .ForMember(d => d.Categories, o => o.Ignore())
                .ForMember(d => d.Screenshots, o => o.Ignore())
                .ForMember(d => d.DownloadLinks, o => o.Ignore());
            CreateMap<Quality, CreateQualityVM>().ReverseMap();
            CreateMap<Screenshot, CreateScreenshotVM>().ReverseMap()
                .ForMember(d => d.Image, o => o.Ignore());
            CreateMap<SlideShow, CreateSlideShowVM>().ReverseMap()
                .ForMember(d => d.Image, o => o.Ignore());

            // Read
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Country, CountryVM>().ReverseMap();
            CreateMap<Director, DirectorVM>().ReverseMap();
            CreateMap<DownloadLink, DownloadLinkVM>().ReverseMap();
            CreateMap<FilmStar, FilmStarVM>().ReverseMap();
            CreateMap<Language, LanguageVM>().ReverseMap();
            CreateMap<Movie, MovieVM>().ReverseMap();
            CreateMap<Quality, QualityVM>().ReverseMap();
            CreateMap<Screenshot, ScreenshotVM>().ReverseMap();
            CreateMap<SlideShow, SlideShowVM>().ReverseMap();
        }
    }
}

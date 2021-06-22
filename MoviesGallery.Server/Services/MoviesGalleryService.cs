using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MoviesGallery.Models;
using MoviesGallery.Repository;
using MoviesGallery.ViewModels;
using MoviesGallery.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesGallery.Server.Services
{
    // Private Fields and Public Constructor.
    partial class MoviesGalleryService : IMoviesGalleryService
    {
        // Private Field(s).
        private readonly IMapper _mapper;
        private readonly string _imageUploadFolder;
        private readonly IConfiguration _configuration;
        private readonly long _imageMinimumSizeInBytes;
        private readonly long _imageMaximumSizeInBytes;
        private readonly string _imageAllowedExtensions;
        private readonly IMoviesGalleryRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        // Public Constructor.
        public MoviesGalleryService(IMapper mapper, IConfiguration configuration, IMoviesGalleryRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _configuration = configuration;
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;

            _imageUploadFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/" + _configuration["ImageConfiguration:SaveTo"]);
            _imageMinimumSizeInBytes = _configuration.GetValue<long>("ImageConfiguration:MinimumSizeInBytes") > 999 ? configuration.GetValue<long>("ImageConfiguration:MinimumSizeInBytes") : 999;
            _imageMaximumSizeInBytes = _configuration.GetValue<long>("ImageConfiguration:MaximumSizeInBytes") < 10000000 ? configuration.GetValue<long>("ImageConfiguration:MaximumSizeInBytes") : 10000000;
            _imageAllowedExtensions = _configuration.GetValue<string>("ImageConfiguration:AllowedExtensions");

            if (!Directory.Exists(_imageUploadFolder))
            {
                Directory.CreateDirectory(_imageUploadFolder);
            }
        }
    }

    // Create
    public partial class MoviesGalleryService
    {
        // Category
        public async Task<string> CreateCategoryAsync(CreateCategoryVM categoryVM)
        {
            Category categoryFromDB = await _repository.ReadCategoryAsync(categoryVM.Name, false);

            if (categoryFromDB == null)
            {
                return await _repository.CreateCategoryAsync(_mapper.Map<Category>(categoryVM));
            }
            else
            {
                if (categoryFromDB.IsActive)
                {
                    return "The category you are trying to add is already exist.";
                }
                else
                {
                    return "The category you are trying to add is already exist with active status false.";
                }
            }
        }

        // Country
        public async Task<string> CreateCountryAsync(CreateCountryVM countryVM)
        {
            Country countryFromDB = await _repository.ReadCountryAsync(countryVM.Name, false);

            if (countryFromDB == null)
            {
                return await _repository.CreateCountryAsync(_mapper.Map<Country>(countryVM));
            }
            else
            {
                if (countryFromDB.IsActive)
                {
                    return "The country you are trying to add is already exist.";
                }
                else
                {
                    return "The country you are trying to add is already exist with active status false.";
                }
            }
        }

        // Director
        public async Task<string> CreateDirectorAsync(CreateDirectorVM directorVM)
        {
            return await _repository.CreateDirectorAsync(_mapper.Map<Director>(directorVM));
        }

        // DownloadLink
        public async Task<string> CreateDownloadLinkAsync(CreateDownloadLinkVM downloadLinkVM)
        {
            Movie movieFromDB = await _repository.ReadMovieAsync(downloadLinkVM.MovieId, false);

            if (movieFromDB == null)
            {
                return $"Movie with Id : {downloadLinkVM.MovieId} does not exist.";
            }
            else
            {
                return await _repository.CreateDownloadLinkAsync(_mapper.Map<DownloadLink>(downloadLinkVM));
            }
        }

        // FilmStar
        public async Task<string> CreateFilmStarAsync(CreateFilmStarVM filmStarVM)
        {
            return await _repository.CreateFilmStarAsync(_mapper.Map<FilmStar>(filmStarVM));
        }

        // Language
        public async Task<string> CreateLanguageAsync(CreateLanguageVM languageVM)
        {
            Language languageFromDB = await _repository.ReadLanguageAsync(languageVM.Name, false);

            if (languageFromDB == null)
            {
                return await _repository.CreateLanguageAsync(_mapper.Map<Language>(languageVM));
            }
            else
            {
                if (languageFromDB.IsActive)
                {
                    return "The language you are trying to add is already exist.";
                }
                else
                {
                    return "The language you are trying to add is already exist with active status false.";
                }
            }
        }

        // Movie
        public async Task<string> CreateMovieAsync(CreateMovieVM movieVM)
        {
            // CountryId
            Country country = await _repository.ReadCountryAsync(movieVM.CountryId, false);
            if (country == null)
            {
                return $"Country with Id : {movieVM.CountryId} does not exist.";
            }
            else
            {
                // DirectorId
                Director director = await _repository.ReadDirectorAsync(movieVM.DirectorId, false);
                if (director == null)
                {
                    return $"Director with Id : {movieVM.DirectorId} does not exist.";
                }
                else
                {
                    // QualityId
                    Quality quality = await _repository.ReadQualityAsync(movieVM.QualityId, false);
                    if (quality == null)
                    {
                        return $"Quality with Id : {movieVM.QualityId} does not exist.";
                    }
                    else
                    {
                        // LanguagesId
                        List<Language> languages = await LanguagesIdToLanguageListAsync(movieVM.LanguagesId);
                        if (languages == null)
                        {
                            return "Language(s) Id does not exist.";
                        }
                        else
                        {
                            // FilmStarsId
                            List<FilmStar> filmStars = await FilmStarsIdToFilmStarListAsync(movieVM.FilmStarsId);
                            if (filmStars == null)
                            {
                                return "FilmStar(s) Id does not exist.";
                            }
                            else
                            {
                                // CategoriesId
                                List<Category> categories = await CategoriesIdToCategoryListAsync(movieVM.CategoriesId);
                                if (categories == null)
                                {
                                    return "Category(s) Id does not exist.";
                                }
                                else
                                {
                                    // DownloadLinks
                                    List<DownloadLink> downloadLinks = LinksToDownloadLinkList(movieVM.DownloadLinks, movieVM.Title);
                                    if (downloadLinks == null)
                                    {
                                        return "Invalid Download Link(s).";
                                    }
                                    else
                                    {
                                        // Cover Image
                                        string image = await SaveImageAsync(movieVM.CoverImage);
                                        if (image == null)
                                        {
                                            return $"Cover Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                                        }
                                        else
                                        {
                                            // Screenshots
                                            List<Screenshot> screenshots = await ImagesToScreenshotListAsync(movieVM.Screenshots, movieVM.Title);
                                            if (screenshots == null)
                                            {
                                                return $"Screenshot(s) are required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                                            }
                                            else
                                            {
                                                Movie movie = _mapper.Map<Movie>(movieVM);
                                                movie.CoverImage = image;
                                                movie.Country = country;
                                                movie.Director = director;
                                                movie.Quality = quality;
                                                movie.Languages = languages;
                                                movie.FilmStars = filmStars;
                                                movie.Categories = categories;
                                                movie.Screenshots = screenshots;
                                                movie.DownloadLinks = downloadLinks;

                                                // Repository Call
                                                string result = await _repository.CreateMovieAsync(movie);

                                                if (result == null || result.Length != 36)
                                                {
                                                    DeleteImage(movie.CoverImage);
                                                    DeleteScreenshot(movie.Screenshots);
                                                }

                                                return result;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Quality
        public async Task<string> CreateQualityAsync(CreateQualityVM qualityVM)
        {
            Quality qualityFromDB = await _repository.ReadQualityAsync(qualityVM.Name, false);

            if (qualityFromDB == null)
            {
                return await _repository.CreateQualityAsync(_mapper.Map<Quality>(qualityVM));
            }
            else
            {
                if (qualityFromDB.IsActive)
                {
                    return "The quality you are trying to add is already exist.";
                }
                else
                {
                    return "The quality you are trying to add is already exist with active status false.";
                }
            }
        }

        // Screenshot
        public async Task<string> CreateScreenshotAsync(CreateScreenshotVM screenshotVM)
        {
            Movie movieFromDB = await _repository.ReadMovieAsync(screenshotVM.MovieId, false);

            if (movieFromDB == null)
            {
                return $"Movie with Id : {screenshotVM.MovieId} does not exist.";
            }
            else
            {
                screenshotVM.Title = screenshotVM.Title == null ? movieFromDB.Title : screenshotVM.Title;
                string image = await SaveImageAsync(screenshotVM.Image);

                if (image == null)
                {
                    return $"Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                }
                else
                {
                    Screenshot screenshot = _mapper.Map<Screenshot>(screenshotVM);
                    screenshot.Image = image;

                    string result = await _repository.CreateScreenshotAsync(screenshot);
                    if (result == null || result.Length != 36)
                    {
                        DeleteImage(screenshot.Image);
                    }

                    return result;
                }
            }
        }

        // SlideShow
        public async Task<string> CreateSlideShowAsync(CreateSlideShowVM slideShowVM)
        {
            Movie movieFromDB = await _repository.ReadMovieAsync(slideShowVM.MovieId, false);

            if (movieFromDB == null)
            {
                return $"Movie with Id : {slideShowVM.MovieId} does not exist.";
            }
            else
            {
                slideShowVM.Title = slideShowVM.Title == null ? movieFromDB.Title : slideShowVM.Title;
                slideShowVM.Description = slideShowVM.Description == null ? movieFromDB.Description : slideShowVM.Description;
                string image = await SaveImageAsync(slideShowVM.Image);

                if (image == null)
                {
                    return $"Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                }
                else
                {
                    SlideShow slideShow = _mapper.Map<SlideShow>(slideShowVM);
                    slideShow.Image = image;

                    string result = await _repository.CreateSlideShowAsync(slideShow);
                    if (result == null || result.Length != 36)
                    {
                        DeleteImage(slideShow.Image);
                    }

                    return result;
                }
            }
        }
    }

    // Read
    public partial class MoviesGalleryService
    {
        // Category - All, Id, Name
        public async Task<List<CategoryVM>> ReadCategoryAsync(bool include, int noOfRecords = 0)
        {
            List<Category> categories = await _repository.ReadCategoryAsync(include, noOfRecords);

            return _mapper.Map<List<CategoryVM>>(categories);
        }
        public async Task<CategoryVM> ReadCategoryAsync(Guid id, bool include)
        {
            Category category = await _repository.ReadCategoryAsync(id, include);

            return category == null ? null : _mapper.Map<CategoryVM>(category);
        }
        public async Task<List<CategoryVM>> ReadCategoryAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Category> categories = await _repository.ReadCategoryAsync(name, include, noOfRecords);

            return _mapper.Map<List<CategoryVM>>(categories);
        }
        public async Task<CategoryVM> ReadCategoryAsync(string name, bool include)
        {
            Category category = await _repository.ReadCategoryAsync(name, include);

            return category == null ? null : _mapper.Map<CategoryVM>(category);
        }
        public async Task<bool> ReadCategoryAsync(Guid id)
        {
            return await _repository.ReadCategoryAsync(id);
        }


        // Country - All, Id, Name
        public async Task<List<CountryVM>> ReadCountryAsync(bool include, int noOfRecords = 0)
        {
            List<Country> countries = await _repository.ReadCountryAsync(include, noOfRecords);

            return _mapper.Map<List<CountryVM>>(countries);
        }
        public async Task<CountryVM> ReadCountryAsync(Guid id, bool include)
        {
            Country country = await _repository.ReadCountryAsync(id, include);

            return country == null ? null : _mapper.Map<CountryVM>(country);
        }
        public async Task<List<CountryVM>> ReadCountryAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Country> countries = await _repository.ReadCountryAsync(name, include, noOfRecords);

            return _mapper.Map<List<CountryVM>>(countries);
        }
        public async Task<CountryVM> ReadCountryAsync(string name, bool include)
        {
            Country country = await _repository.ReadCountryAsync(name, include);

            return country == null ? null : _mapper.Map<CountryVM>(country);
        }
        public async Task<bool> ReadCountryAsync(Guid id)
        {
            return await _repository.ReadCountryAsync(id);
        }


        // Director - All, Id, Name
        public async Task<List<DirectorVM>> ReadDirectorAsync(bool include, int noOfRecords = 0)
        {
            List<Director> directors = await _repository.ReadDirectorAsync(include, noOfRecords);

            return _mapper.Map<List<DirectorVM>>(directors);
        }
        public async Task<DirectorVM> ReadDirectorAsync(Guid id, bool include)
        {
            Director director = await _repository.ReadDirectorAsync(id, include);

            return director == null ? null : _mapper.Map<DirectorVM>(director);
        }
        public async Task<List<DirectorVM>> ReadDirectorAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Director> directors = await _repository.ReadDirectorAsync(name, include, noOfRecords);

            return _mapper.Map<List<DirectorVM>>(directors);
        }
        public async Task<DirectorVM> ReadDirectorAsync(string name, bool include)
        {
            Director director = await _repository.ReadDirectorAsync(name, include);

            return director == null ? null : _mapper.Map<DirectorVM>(director);
        }
        public async Task<bool> ReadDirectorAsync(Guid id)
        {
            return await _repository.ReadDirectorAsync(id);
        }


        // DownloadLink - All, Id, Title
        public async Task<List<DownloadLinkVM>> ReadDownloadLinkAsync(bool include, int noOfRecords = 0)
        {
            List<DownloadLink> downloadLinks = await _repository.ReadDownloadLinkAsync(include, noOfRecords);

            return _mapper.Map<List<DownloadLinkVM>>(downloadLinks);
        }
        public async Task<DownloadLinkVM> ReadDownloadLinkAsync(Guid id, bool include)
        {
            DownloadLink downloadLink = await _repository.ReadDownloadLinkAsync(id, include);

            return downloadLink == null ? null : _mapper.Map<DownloadLinkVM>(downloadLink);
        }
        public async Task<List<DownloadLinkVM>> ReadDownloadLinkAsync(string title, bool include, int noOfRecords = 0)
        {
            List<DownloadLink> downloadLinks = await _repository.ReadDownloadLinkAsync(title, include, noOfRecords);

            return _mapper.Map<List<DownloadLinkVM>>(downloadLinks);
        }
        public async Task<DownloadLinkVM> ReadDownloadLinkAsync(string title, bool include)
        {
            DownloadLink downloadLink = await _repository.ReadDownloadLinkAsync(title, include);

            return downloadLink == null ? null : _mapper.Map<DownloadLinkVM>(downloadLink);
        }
        public async Task<bool> ReadDownloadLinkAsync(Guid id)
        {
            return await _repository.ReadDownloadLinkAsync(id);
        }


        // FilmStar - All, Id, Name
        public async Task<List<FilmStarVM>> ReadFilmStarAsync(bool include, int noOfRecords = 0)
        {
            List<FilmStar> filmStars = await _repository.ReadFilmStarAsync(include, noOfRecords);

            return _mapper.Map<List<FilmStarVM>>(filmStars);
        }
        public async Task<FilmStarVM> ReadFilmStarAsync(Guid id, bool include)
        {
            FilmStar filmStar = await _repository.ReadFilmStarAsync(id, include);

            return filmStar == null ? null : _mapper.Map<FilmStarVM>(filmStar);
        }
        public async Task<List<FilmStarVM>> ReadFilmStarAsync(string name, bool include, int noOfRecords = 0)
        {
            List<FilmStar> filmStars = await _repository.ReadFilmStarAsync(name, include, noOfRecords);

            return _mapper.Map<List<FilmStarVM>>(filmStars);
        }
        public async Task<FilmStarVM> ReadFilmStarAsync(string name, bool include)
        {
            FilmStar filmStar = await _repository.ReadFilmStarAsync(name, include);

            return filmStar == null ? null : _mapper.Map<FilmStarVM>(filmStar);
        }
        public async Task<bool> ReadFilmStarAsync(Guid id)
        {
            return await _repository.ReadFilmStarAsync(id);
        }


        // Language - All, Id, Name
        public async Task<List<LanguageVM>> ReadLanguageAsync(bool include, int noOfRecords = 0)
        {
            List<Language> languages = await _repository.ReadLanguageAsync(include, noOfRecords);

            return _mapper.Map<List<LanguageVM>>(languages);
        }
        public async Task<LanguageVM> ReadLanguageAsync(Guid id, bool include)
        {
            Language language = await _repository.ReadLanguageAsync(id, include);

            return language == null ? null : _mapper.Map<LanguageVM>(language);
        }
        public async Task<List<LanguageVM>> ReadLanguageAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Language> languages = await _repository.ReadLanguageAsync(name, include, noOfRecords);

            return _mapper.Map<List<LanguageVM>>(languages);
        }
        public async Task<LanguageVM> ReadLanguageAsync(string name, bool include)
        {
            Language language = await _repository.ReadLanguageAsync(name, include);

            return language == null ? null : _mapper.Map<LanguageVM>(language);
        }
        public async Task<bool> ReadLanguageAsync(Guid id)
        {
            return await _repository.ReadLanguageAsync(id);
        }


        // Movie - All, Id, Title
        public async Task<List<MovieVM>> ReadMovieAsync(bool include, int noOfRecords = 0)
        {
            List<Movie> movies = await _repository.ReadMovieAsync(include, noOfRecords);

            return _mapper.Map<List<MovieVM>>(movies);
        }
        public async Task<MovieVM> ReadMovieAsync(Guid id, bool include)
        {
            Movie movie = await _repository.ReadMovieAsync(id, include);

            return movie == null ? null : _mapper.Map<MovieVM>(movie);
        }
        public async Task<List<MovieVM>> ReadMovieAsync(string title, bool include, int noOfRecords = 0)
        {
            List<Movie> movies = await _repository.ReadMovieAsync(title, include, noOfRecords);

            return _mapper.Map<List<MovieVM>>(movies);
        }
        public async Task<MovieVM> ReadMovieAsync(string title, bool include)
        {
            Movie movie = await _repository.ReadMovieAsync(title, include);

            return movie == null ? null : _mapper.Map<MovieVM>(movie);
        }
        public async Task<bool> ReadMovieAsync(Guid id)
        {
            return await _repository.ReadMovieAsync(id);
        }


        // Quality - All, Id, Name
        public async Task<List<QualityVM>> ReadQualityAsync(bool include, int noOfRecords = 0)
        {
            List<Quality> qualities = await _repository.ReadQualityAsync(include, noOfRecords);

            return _mapper.Map<List<QualityVM>>(qualities);
        }
        public async Task<QualityVM> ReadQualityAsync(Guid id, bool include)
        {
            Quality quality = await _repository.ReadQualityAsync(id, include);

            return quality == null ? null : _mapper.Map<QualityVM>(quality);
        }
        public async Task<List<QualityVM>> ReadQualityAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Quality> qualities = await _repository.ReadQualityAsync(name, include, noOfRecords);

            return _mapper.Map<List<QualityVM>>(qualities);
        }
        public async Task<QualityVM> ReadQualityAsync(string name, bool include)
        {
            Quality quality = await _repository.ReadQualityAsync(name, include);

            return quality == null ? null : _mapper.Map<QualityVM>(quality);
        }
        public async Task<bool> ReadQualityAsync(Guid id)
        {
            return await _repository.ReadQualityAsync(id);
        }


        // Screenshot - All, Id, Title
        public async Task<List<ScreenshotVM>> ReadScreenshotAsync(bool include, int noOfRecords = 0)
        {
            List<Screenshot> screenshots = await _repository.ReadScreenshotAsync(include, noOfRecords);

            return _mapper.Map<List<ScreenshotVM>>(screenshots);
        }
        public async Task<ScreenshotVM> ReadScreenshotAsync(Guid id, bool include)
        {
            Screenshot screenshot = await _repository.ReadScreenshotAsync(id, include);

            return screenshot == null ? null : _mapper.Map<ScreenshotVM>(screenshot);
        }
        public async Task<List<ScreenshotVM>> ReadScreenshotAsync(string title, bool include, int noOfRecords = 0)
        {
            List<Screenshot> screenshots = await _repository.ReadScreenshotAsync(title, include, noOfRecords);

            return _mapper.Map<List<ScreenshotVM>>(screenshots);
        }
        public async Task<ScreenshotVM> ReadScreenshotAsync(string title, bool include)
        {
            Screenshot screenshot = await _repository.ReadScreenshotAsync(title, include);

            return screenshot == null ? null : _mapper.Map<ScreenshotVM>(screenshot);
        }
        public async Task<bool> ReadScreenshotAsync(Guid id)
        {
            return await _repository.ReadScreenshotAsync(id);
        }


        // SlideShow - All, Id, Title
        public async Task<List<SlideShowVM>> ReadSlideShowAsync(bool include, int noOfRecords = 0)
        {
            List<SlideShow> slideShows = await _repository.ReadSlideShowAsync(include, noOfRecords);

            return _mapper.Map<List<SlideShowVM>>(slideShows);
        }
        public async Task<SlideShowVM> ReadSlideShowAsync(Guid id, bool include)
        {
            SlideShow slideShow = await _repository.ReadSlideShowAsync(id, include);

            return slideShow == null ? null : _mapper.Map<SlideShowVM>(slideShow);
        }
        public async Task<List<SlideShowVM>> ReadSlideShowAsync(string title, bool include, int noOfRecords = 0)
        {
            List<SlideShow> slideShows = await _repository.ReadSlideShowAsync(title, include, noOfRecords);

            return _mapper.Map<List<SlideShowVM>>(slideShows);
        }
        public async Task<SlideShowVM> ReadSlideShowAsync(string title, bool include)
        {
            SlideShow slideShow = await _repository.ReadSlideShowAsync(title, include);

            return slideShow == null ? null : _mapper.Map<SlideShowVM>(slideShow);
        }
        public async Task<bool> ReadSlideShowAsync(Guid id)
        {
            return await _repository.ReadSlideShowAsync(id);
        }
    }

    // Update
    public partial class MoviesGalleryService
    {
        // Category
        public async Task<string> UpdateCategoryAsync(UpdateCategoryVM categoryVM)
        {
            Category categoryFromDB = await _repository.ReadCategoryAsync(categoryVM.Id, true);

            if (categoryFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (categoryVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(categoryVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        categoryFromDB.Movies = movies;
                    }
                }

                categoryFromDB.Name = string.IsNullOrEmpty(categoryVM.Name) ? categoryFromDB.Name : categoryVM.Name;
                categoryFromDB.Description = string.IsNullOrEmpty(categoryVM.Description) ? categoryFromDB.Description : categoryVM.Description;
                categoryFromDB.IsActive = categoryVM.IsActive ?? categoryFromDB.IsActive;

                return await _repository.UpdateCategoryAsync(categoryFromDB);
            }
        }

        // Country
        public async Task<string> UpdateCountryAsync(UpdateCountryVM countryVM)
        {
            Country countryFromDB = await _repository.ReadCountryAsync(countryVM.Id, true);

            if (countryFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (countryVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(countryVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        countryFromDB.Movies = movies;
                    }
                }

                countryFromDB.Name = string.IsNullOrEmpty(countryVM.Name) ? countryFromDB.Name : countryVM.Name;
                countryFromDB.Description = string.IsNullOrEmpty(countryVM.Description) ? countryFromDB.Description : countryVM.Description;
                countryFromDB.IsActive = countryVM.IsActive ?? countryFromDB.IsActive;

                return await _repository.UpdateCountryAsync(countryFromDB);
            }
        }

        // Director
        public async Task<string> UpdateDirectorAsync(UpdateDirectorVM directorVM)
        {
            Director directorFromDB = await _repository.ReadDirectorAsync(directorVM.Id, true);

            if (directorFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (directorVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(directorVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        directorFromDB.Movies = movies;
                    }
                }

                directorFromDB.Name = string.IsNullOrEmpty(directorVM.Name) ? directorFromDB.Name : directorVM.Name;
                directorFromDB.Description = string.IsNullOrEmpty(directorVM.Description) ? directorFromDB.Description : directorVM.Description;
                directorFromDB.IsActive = directorVM.IsActive ?? directorFromDB.IsActive;

                return await _repository.UpdateDirectorAsync(directorFromDB);
            }
        }

        // DownloadLink
        public async Task<string> UpdateDownloadLinkAsync(UpdateDownloadLinkVM downloadLinkVM)
        {
            DownloadLink downloadLinkFromDB = await _repository.ReadDownloadLinkAsync(downloadLinkVM.Id, true);

            if (downloadLinkFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (downloadLinkVM.MovieId != Guid.Empty)
                {
                    Movie movie = await _repository.ReadMovieAsync(downloadLinkVM.MovieId, false);

                    if (movie == null)
                    {
                        return "Movie Id does not exist.";
                    }
                    else
                    {
                        downloadLinkFromDB.Movie = movie;
                    }
                }

                downloadLinkFromDB.Title = string.IsNullOrEmpty(downloadLinkVM.Title) ? downloadLinkFromDB.Title : downloadLinkVM.Title;
                downloadLinkFromDB.Description = string.IsNullOrEmpty(downloadLinkVM.Description) ? downloadLinkFromDB.Description : downloadLinkVM.Description;
                downloadLinkFromDB.Link = string.IsNullOrEmpty(downloadLinkVM.Link) ? downloadLinkFromDB.Link : downloadLinkVM.Link;
                downloadLinkFromDB.IsActive = downloadLinkVM.IsActive ?? downloadLinkFromDB.IsActive;

                return await _repository.UpdateDownloadLinkAsync(downloadLinkFromDB);
            }
        }

        // FilmStar
        public async Task<string> UpdateFilmStarAsync(UpdateFilmStarVM filmStarVM)
        {
            FilmStar filmStarFromDB = await _repository.ReadFilmStarAsync(filmStarVM.Id, true);

            if (filmStarFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (filmStarVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(filmStarVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        filmStarFromDB.Movies = movies;
                    }
                }

                filmStarFromDB.Name = string.IsNullOrEmpty(filmStarVM.Name) ? filmStarFromDB.Name : filmStarVM.Name;
                filmStarFromDB.Description = string.IsNullOrEmpty(filmStarVM.Description) ? filmStarFromDB.Description : filmStarVM.Description;
                filmStarFromDB.IsActive = filmStarVM.IsActive ?? filmStarFromDB.IsActive;

                return await _repository.UpdateFilmStarAsync(filmStarFromDB);
            }
        }

        // Language
        public async Task<string> UpdateLanguageAsync(UpdateLanguageVM languageVM)
        {
            Language languageFromDB = await _repository.ReadLanguageAsync(languageVM.Id, true);

            if (languageFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (languageVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(languageVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        languageFromDB.Movies = movies;
                    }
                }

                languageFromDB.Name = string.IsNullOrEmpty(languageVM.Name) ? languageFromDB.Name : languageVM.Name;
                languageFromDB.Description = string.IsNullOrEmpty(languageVM.Description) ? languageFromDB.Description : languageVM.Description;
                languageFromDB.IsActive = languageVM.IsActive ?? languageFromDB.IsActive;

                return await _repository.UpdateLanguageAsync(languageFromDB);
            }
        }

        // Movie
        public async Task<string> UpdateMovieAsync(UpdateMovieVM movieVM)
        {
            Movie movieFromDB = await _repository.ReadMovieAsync(movieVM.Id, true);

            if (movieFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                movieFromDB.Title = string.IsNullOrEmpty(movieVM.Title) ? movieFromDB.Title : movieVM.Title;
                movieFromDB.StoryLine = string.IsNullOrEmpty(movieVM.StoryLine) ? movieFromDB.StoryLine : movieVM.StoryLine;
                movieFromDB.Description = string.IsNullOrEmpty(movieVM.Description) ? movieFromDB.Description : movieVM.Description;
                movieFromDB.Size = movieVM.Size ?? movieFromDB.Size;
                movieFromDB.SampleDownloadLink = string.IsNullOrEmpty(movieVM.SampleDownloadLink) ? movieFromDB.SampleDownloadLink : movieVM.SampleDownloadLink;
                movieFromDB.DateOfRelease = movieVM.DateOfRelease ?? movieFromDB.DateOfRelease;
                movieFromDB.LastUpdatedOn = DateTime.Now;
                movieFromDB.IMDB = movieVM.IMDB ?? movieFromDB.IMDB;
                movieFromDB.Trailer = string.IsNullOrEmpty(movieVM.Trailer) ? movieFromDB.Trailer : movieVM.Trailer;
                movieFromDB.IsActive = movieVM.IsActive ?? movieFromDB.IsActive;

                // CountryId
                if (movieVM.CountryId != Guid.Empty)
                {
                    Country country = await _repository.ReadCountryAsync(movieVM.CountryId, false);

                    if (country == null)
                    {
                        return $"Country with Id : {movieVM.CountryId} does not exist.";
                    }
                    else
                    {
                        movieFromDB.Country = country;
                    }
                }

                // DirectorId
                if (movieVM.DirectorId != Guid.Empty)
                {
                    Director director = await _repository.ReadDirectorAsync(movieVM.DirectorId, false);

                    if (director == null)
                    {
                        return $"Director with Id : {movieVM.DirectorId} does not exist.";
                    }
                    else
                    {
                        movieFromDB.Director = director;
                    }
                }

                // QualityId
                if (movieVM.QualityId != Guid.Empty)
                {
                    Quality quality = await _repository.ReadQualityAsync(movieVM.QualityId, false);

                    if (quality == null)
                    {
                        return $"Quality with Id : {movieVM.QualityId} does not exist.";
                    }
                    else
                    {
                        movieFromDB.Quality = quality;
                    }
                }

                // LanguagesId
                if (movieVM.LanguagesId != null)
                {
                    List<Language> languages = await LanguagesIdToLanguageListAsync(movieVM.LanguagesId);

                    if (languages == null)
                    {
                        return "Language(s) Id does not exist.";
                    }
                    else
                    {
                        movieFromDB.Languages = languages;
                    }
                }

                // FilmStarsId
                if (movieVM.FilmStarsId != null)
                {
                    List<FilmStar> filmStars = await FilmStarsIdToFilmStarListAsync(movieVM.FilmStarsId);

                    if (filmStars == null)
                    {
                        return "FilmStar(s) Id does not exist.";
                    }
                    else
                    {
                        movieFromDB.FilmStars = filmStars;
                    }
                }

                // CategoriesId
                if (movieVM.CategoriesId != null)
                {
                    List<Category> categories = await CategoriesIdToCategoryListAsync(movieVM.CategoriesId);

                    if (categories == null)
                    {
                        return "Category(s) Id does not exist.";
                    }
                    else
                    {
                        movieFromDB.Categories = categories;
                    }
                }

                // DownloadLinks
                if (movieVM.DownloadLinks != null)
                {
                    List<DownloadLink> downloadLinks = LinksToDownloadLinkList(movieVM.DownloadLinks, movieVM.Title);

                    if (downloadLinks == null)
                    {
                        return "Invalid Download Link(s).";
                    }
                    else
                    {
                        movieFromDB.DownloadLinks = downloadLinks;
                    }
                }

                // Cover Image
                string coverImageFromDB = string.Empty;
                if (movieVM.CoverImage != null)
                {
                    string image = await SaveImageAsync(movieVM.CoverImage);

                    if (image == null)
                    {
                        return $"Cover Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                    }
                    else
                    {
                        coverImageFromDB = movieFromDB.CoverImage;
                        movieFromDB.CoverImage = image;
                    }
                }

                // Screenshots
                List<Screenshot> screenshotsFromDB = null;
                if (movieVM.Screenshots != null)
                {
                    List<Screenshot> screenshots = await ImagesToScreenshotListAsync(movieVM.Screenshots, movieVM.Title);

                    if (screenshots == null)
                    {
                        return $"Screenshot(s) are required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                    }
                    else
                    {
                        screenshotsFromDB = movieFromDB.Screenshots;
                        movieFromDB.Screenshots = screenshots;
                    }
                }

                // Repository Call
                string result = await _repository.UpdateMovieAsync(movieFromDB);

                if (movieVM.CoverImage != null)
                {
                    if(result.Length == 36)
                    {
                        DeleteImage(coverImageFromDB);
                        DeleteScreenshot(screenshotsFromDB);
                    }
                    else
                    {
                        DeleteImage(movieFromDB.CoverImage);
                        DeleteScreenshot(movieFromDB.Screenshots);
                    }
                }

                return result;
            }
        }

        // Quality
        public async Task<string> UpdateQualityAsync(UpdateQualityVM qualityVM)
        {
            Quality qualityFromDB = await _repository.ReadQualityAsync(qualityVM.Id, true);

            if (qualityFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                if (qualityVM.MoviesId != null)
                {
                    List<Movie> movies = await MoviesIdToMovieListAsync(qualityVM.MoviesId);

                    if (movies == null)
                    {
                        return "Movie(s) Id does not exist.";
                    }
                    else
                    {
                        qualityFromDB.Movies = movies;
                    }
                }

                qualityFromDB.Name = string.IsNullOrEmpty(qualityVM.Name) ? qualityFromDB.Name : qualityVM.Name;
                qualityFromDB.Description = string.IsNullOrEmpty(qualityVM.Description) ? qualityFromDB.Description : qualityVM.Description;
                qualityFromDB.IsActive = qualityVM.IsActive ?? qualityFromDB.IsActive;

                return await _repository.UpdateQualityAsync(qualityFromDB);
            }
        }

        // Screenshot
        public async Task<string> UpdateScreenshotAsync(UpdateScreenshotVM screenshotVM)
        {
            Screenshot screenshotFromDB = await _repository.ReadScreenshotAsync(screenshotVM.Id, true);

            if (screenshotFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                string imageFromDB = string.Empty;

                if (screenshotVM.Image != null)
                {
                    string image = await SaveImageAsync(screenshotVM.Image);

                    if (image == null)
                    {
                        return $"Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                    }
                    else
                    {
                        imageFromDB = screenshotFromDB.Image;
                        screenshotFromDB.Image = image;
                    }
                }

                if (screenshotFromDB.MovieId != Guid.Empty)
                {
                    Movie movie = await _repository.ReadMovieAsync(screenshotFromDB.MovieId, false);

                    if (movie == null)
                    {
                        return "Movie Id does not exist.";
                    }
                    else
                    {
                        screenshotFromDB.Movie = movie;
                    }
                }

                screenshotFromDB.Title = string.IsNullOrEmpty(screenshotVM.Title) ? screenshotFromDB.Title : screenshotVM.Title;
                screenshotFromDB.Description = string.IsNullOrEmpty(screenshotVM.Description) ? screenshotFromDB.Description : screenshotVM.Description;
                screenshotFromDB.IsActive = screenshotVM.IsActive ?? screenshotFromDB.IsActive;

                string result = await _repository.UpdateScreenshotAsync(screenshotFromDB);

                if (screenshotVM.Image != null)
                {
                    if (result.Length == 36)
                    {
                        DeleteImage(imageFromDB);
                    }
                    else
                    {
                        DeleteImage(screenshotFromDB.Image);
                    }
                }

                return result;
            }
        }

        // SlideShow
        public async Task<string> UpdateSlideShowAsync(UpdateSlideShowVM slideShowVM)
        {
            SlideShow slideShowFromDB = await _repository.ReadSlideShowAsync(slideShowVM.Id, true);

            if (slideShowFromDB == null)
            {
                return "NotFound";
            }
            else
            {
                string imageFromDB = string.Empty;

                if (slideShowVM.Image != null)
                {
                    string image = await SaveImageAsync(slideShowVM.Image);

                    if (image == null)
                    {
                        return $"Image is required with an minimum size of {((_imageMinimumSizeInBytes) / 1000)} KB and maximum size of {((_imageMaximumSizeInBytes) / 1000) / 1000} MB in {_imageAllowedExtensions} extension.";
                    }
                    else
                    {
                        imageFromDB = slideShowFromDB.Image;
                        slideShowFromDB.Image = image;
                    }
                }

                if (slideShowFromDB.MovieId != Guid.Empty)
                {
                    Movie movie = await _repository.ReadMovieAsync(slideShowFromDB.MovieId, false);

                    if (movie == null)
                    {
                        return "Movie Id does not exist.";
                    }
                    else
                    {
                        slideShowFromDB.Movie = movie;
                    }
                }

                slideShowFromDB.Title = string.IsNullOrEmpty(slideShowVM.Title) ? slideShowFromDB.Title : slideShowVM.Title;
                slideShowFromDB.Description = string.IsNullOrEmpty(slideShowVM.Description) ? slideShowFromDB.Description : slideShowVM.Description;
                slideShowFromDB.Order = slideShowVM.Order ?? slideShowFromDB.Order;
                slideShowFromDB.IsActive = slideShowVM.IsActive ?? slideShowFromDB.IsActive;

                string result = await _repository.UpdateSlideShowAsync(slideShowFromDB);

                if(slideShowVM.Image != null)
                {
                    if(result.Length == 36)
                    {
                        DeleteImage(imageFromDB);
                    }
                    else
                    {
                        DeleteImage(slideShowFromDB.Image);
                    }
                }

                return result;
            }
        }
    }

    // Delete
    public partial class MoviesGalleryService
    {
        // Category
        public async Task<string> DeleteCategoryAsync(Guid id)
        {
            if (await _repository.ReadCategoryAsync(id))
            {
                string result = await _repository.DeleteCategoryAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // Country
        public async Task<string> DeleteCountryAsync(Guid id)
        {
            if (await _repository.ReadCountryAsync(id))
            {
                string result = await _repository.DeleteCountryAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // Director
        public async Task<string> DeleteDirectorAsync(Guid id)
        {
            if (await _repository.ReadDirectorAsync(id))
            {
                string result = await _repository.DeleteDirectorAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // DownloadLink
        public async Task<string> DeleteDownloadLinkAsync(Guid id)
        {
            if (await _repository.ReadDownloadLinkAsync(id))
            {
                string result = await _repository.DeleteDownloadLinkAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // FilmStar
        public async Task<string> DeleteFilmStarAsync(Guid id)
        {
            if (await _repository.ReadFilmStarAsync(id))
            {
                string result = await _repository.DeleteFilmStarAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // Language
        public async Task<string> DeleteLanguageAsync(Guid id)
        {
            if (await _repository.ReadLanguageAsync(id))
            {
                string result = await _repository.DeleteLanguageAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // Movie
        public async Task<string> DeleteMovieAsync(Guid id)
        {
            if (await _repository.ReadMovieAsync(id))
            {
                Movie movie = await _repository.DeleteMovieAsync(id);

                if (movie != null)
                {
                    DeleteImage(movie.CoverImage);
                    DeleteScreenshot(movie.Screenshots);

                    return movie.Id.ToString();
                }
            }

            return null;
        }

        // Quality
        public async Task<string> DeleteQualityAsync(Guid id)
        {
            if (await _repository.ReadQualityAsync(id))
            {
                string result = await _repository.DeleteQualityAsync(id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        // Screenshot
        public async Task<string> DeleteScreenshotAsync(Guid id)
        {
            if (await _repository.ReadScreenshotAsync(id))
            {
                Screenshot screenshot = await _repository.DeleteScreenshotAsync(id);

                if (screenshot != null)
                {
                    DeleteImage(screenshot.Image);

                    return screenshot.Id.ToString();
                }
            }

            return null;
        }

        // SlideShow
        public async Task<string> DeleteSlideShowAsync(Guid id)
        {
            if (await _repository.ReadSlideShowAsync(id))
            {
                SlideShow slideShow = await _repository.DeleteSlideShowAsync(id);

                if (slideShow != null)
                {
                    DeleteImage(slideShow.Image);

                    return slideShow.Id.ToString();
                }
            }

            return null;
        }
    }

    // Private Methods
    public partial class MoviesGalleryService
    {
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            if (image != null && image.Length >= _imageMinimumSizeInBytes && image.Length <= _imageMaximumSizeInBytes)
            {
                string imageExtension = Path.GetExtension(image.FileName);

                if (_imageAllowedExtensions.Contains(imageExtension))
                {
                    string fileName = Guid.NewGuid().ToString() + imageExtension;
                    string filePath = Path.Combine(_imageUploadFolder, fileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    return _configuration["ImageConfiguration:SaveTo"] + "/" + fileName;
                }
            }

            return null;
        }

        private async Task<List<string>> SaveImageAsync(List<IFormFile> images)
        {
            if (images != null)
            {
                List<string> filePaths = new List<string>();

                foreach (IFormFile image in images)
                {
                    string filePath = await SaveImageAsync(image);

                    if (filePath == null)
                    {
                        return null;
                    }
                    else
                    {
                        filePaths.Add(filePath);
                    }
                }

                return filePaths;
            }

            return null;
        }

        private async Task<List<Language>> LanguagesIdToLanguageListAsync(List<Guid> languagesId)
        {
            List<Language> languages = await _repository.ReadLanguageAsync(false, 0);

            if (languages.Count() > 0)
            {
                List<Language> selectedLanguages = languages.Where(x => languagesId.Contains(x.Id)).ToList();

                return selectedLanguages.Count() == languagesId.Count() ? selectedLanguages : null;
            }
            else
            {
                return null;
            }
        }

        private async Task<List<FilmStar>> FilmStarsIdToFilmStarListAsync(List<Guid> filmStarsId)
        {
            List<FilmStar> filmStars = await _repository.ReadFilmStarAsync(false, 0);

            if (filmStars.Count() > 0)
            {
                List<FilmStar> selectedFilmStars = filmStars.Where(x => filmStarsId.Contains(x.Id)).ToList();

                return selectedFilmStars.Count() == filmStarsId.Count() ? selectedFilmStars : null;
            }
            else
            {
                return null;
            }
        }

        private async Task<List<Category>> CategoriesIdToCategoryListAsync(List<Guid> categoriesId)
        {
            List<Category> categories = await _repository.ReadCategoryAsync(false, 0);

            if (categories.Count() > 0)
            {
                List<Category> selectedCategories = categories.Where(x => categoriesId.Contains(x.Id)).ToList();

                return selectedCategories.Count() == categoriesId.Count() ? selectedCategories : null;
            }
            else
            {
                return null;
            }
        }

        private async Task<List<Screenshot>> ImagesToScreenshotListAsync(List<IFormFile> images, string title)
        {
            if (images != null)
            {
                List<string> filePaths = await SaveImageAsync(images);

                if (filePaths != null && filePaths.Count() > 0)
                {
                    List<Screenshot> screenshots = new List<Screenshot>();

                    foreach (string filePath in filePaths)
                    {
                        screenshots.Add(new Screenshot()
                        {
                            Title = title,
                            Image = filePath
                        });
                    }

                    return screenshots;
                }
            }

            return null;
        }

        private List<DownloadLink> LinksToDownloadLinkList(List<string> links, string title)
        {
            if (links != null && links.Count() > 0)
            {
                List<DownloadLink> downloadLinks = new List<DownloadLink>();

                foreach (string link in links)
                {
                    if (string.IsNullOrEmpty(link))
                    {
                        return null;
                    }

                    downloadLinks.Add(new DownloadLink()
                    {
                        Title = title,
                        Link = link
                    });
                }

                return downloadLinks;
            }

            return null;
        }

        private void DeleteImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine("wwwroot", fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void DeleteScreenshot(List<Screenshot> screenshots)
        {
            if (screenshots != null && screenshots.Count() > 0)
            {
                foreach (Screenshot screenshot in screenshots)
                {
                    DeleteImage(screenshot.Image);
                }
            }
        }

        private async Task<List<Movie>> MoviesIdToMovieListAsync(List<Guid> moviesId)
        {
            List<Movie> movies = await _repository.ReadMovieAsync(false, 0);

            if (movies.Count() > 0)
            {
                List<Movie> selectedMovies = movies.Where(x => moviesId.Contains(x.Id)).ToList();

                return selectedMovies.Count() == moviesId.Count() ? selectedMovies : null;
            }
            else
            {
                return new List<Movie>();
            }
        }
    }
}
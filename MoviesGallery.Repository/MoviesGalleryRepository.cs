using Microsoft.EntityFrameworkCore;
using MoviesGallery.Context;
using MoviesGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesGallery.Repository
{
    // Private Fields and Public Constructor.
    public partial class MoviesGalleryRepository : IMoviesGalleryRepository
    {
        // Private Field(s).
        private readonly ApplicationDbContext _context;


        // Public Constructor.
        public MoviesGalleryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }

    // Create
    public partial class MoviesGalleryRepository
    {
        // Category
        public async Task<string> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            _context.Entry(category).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? category.Id.ToString() : null;
        }

        // Country
        public async Task<string> CreateCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            _context.Entry(country).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? country.Id.ToString() : null;
        }

        // Director
        public async Task<string> CreateDirectorAsync(Director director)
        {
            _context.Directors.Add(director);
            _context.Entry(director).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? director.Id.ToString() : null;
        }

        // DownloadLink
        public async Task<string> CreateDownloadLinkAsync(DownloadLink downloadLink)
        {
            _context.DownloadLinks.Add(downloadLink);
            _context.Entry(downloadLink).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? downloadLink.Id.ToString() : null;
        }

        // FilmStar
        public async Task<string> CreateFilmStarAsync(FilmStar filmStar)
        {
            _context.FilmStars.Add(filmStar);
            _context.Entry(filmStar).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? filmStar.Id.ToString() : null;
        }

        // Language
        public async Task<string> CreateLanguageAsync(Language language)
        {
            _context.Languages.Add(language);
            _context.Entry(language).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? language.Id.ToString() : null;
        }

        // Movie
        public async Task<string> CreateMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.Entry(movie).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? movie.Id.ToString() : null;
        }

        // Quality
        public async Task<string> CreateQualityAsync(Quality quality)
        {
            _context.Qualities.Add(quality);
            _context.Entry(quality).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? quality.Id.ToString() : null;
        }

        // Screenshot
        public async Task<string> CreateScreenshotAsync(Screenshot screenshot)
        {
            _context.Screenshots.Add(screenshot);
            _context.Entry(screenshot).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? screenshot.Id.ToString() : null;
        }

        // SlideShow
        public async Task<string> CreateSlideShowAsync(SlideShow slideShow)
        {
            _context.SlideShows.Add(slideShow);
            _context.Entry(slideShow).State = EntityState.Added;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? slideShow.Id.ToString() : null;
        }
    }

    // Read
    public partial class MoviesGalleryRepository
    {
        // Category - All, Id, Name
        public async Task<List<Category>> ReadCategoryAsync(bool include, int noOfRecords = 0)
        {
            List<Category> categories;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    categories = await _context.Categories.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    categories = await _context.Categories.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                categories = FilterCategory(categories);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    categories = await _context.Categories.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return categories;
        }
        public async Task<Category> ReadCategoryAsync(Guid id, bool include)
        {
            Category category;

            if (include)
            {
                category = await _context.Categories.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                category = FilterCategory(category);
            }
            else
            {
                category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            }

            return category;
        }
        public async Task<List<Category>> ReadCategoryAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Category> categories;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    categories = await _context.Categories.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    categories = await _context.Categories.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                categories = FilterCategory(categories);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    categories = await _context.Categories.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    categories = await _context.Categories.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return categories;
        }
        public async Task<Category> ReadCategoryAsync(string name, bool include)
        {
            Category category;
            name = name.ToUpper();

            if (include)
            {
                category = await _context.Categories.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                category = FilterCategory(category);
            }
            else
            {
                category = await _context.Categories.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return category;
        }
        public async Task<bool> ReadCategoryAsync(Guid id)
        {
            Category category = await ReadCategoryAsync(id, false);

            return category == null ? false : true;
        }


        // Country - All, Id, Name
        public async Task<List<Country>> ReadCountryAsync(bool include, int noOfRecords = 0)
        {
            List<Country> countries;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    countries = await _context.Countries.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    countries = await _context.Countries.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                countries = FilterCountry(countries);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    countries = await _context.Countries.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    countries = await _context.Countries.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return countries;
        }
        public async Task<Country> ReadCountryAsync(Guid id, bool include)
        {
            Country country;

            if (include)
            {
                country = await _context.Countries.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                country = FilterCountry(country);
            }
            else
            {
                country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            }

            return country;
        }
        public async Task<List<Country>> ReadCountryAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Country> countries;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    countries = await _context.Countries.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    countries = await _context.Countries.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                countries = FilterCountry(countries);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    countries = await _context.Countries.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    countries = await _context.Countries.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return countries;
        }
        public async Task<Country> ReadCountryAsync(string name, bool include)
        {
            Country country;
            name = name.ToUpper();

            if (include)
            {
                country = await _context.Countries.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                country = FilterCountry(country);
            }
            else
            {
                country = await _context.Countries.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return country;
        }
        public async Task<bool> ReadCountryAsync(Guid id)
        {
            Country country = await ReadCountryAsync(id, false);

            return country == null ? false : true;
        }


        // Director - All, Id, Name
        public async Task<List<Director>> ReadDirectorAsync(bool include, int noOfRecords = 0)
        {
            List<Director> directors;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    directors = await _context.Directors.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    directors = await _context.Directors.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                directors = FilterDirector(directors);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    directors = await _context.Directors.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    directors = await _context.Directors.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return directors;
        }
        public async Task<Director> ReadDirectorAsync(Guid id, bool include)
        {
            Director director;

            if (include)
            {
                director = await _context.Directors.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                director = FilterDirector(director);
            }
            else
            {
                director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == id);
            }

            return director;
        }
        public async Task<List<Director>> ReadDirectorAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Director> directors;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    directors = await _context.Directors.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    directors = await _context.Directors.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                directors = FilterDirector(directors);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    directors = await _context.Directors.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    directors = await _context.Directors.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return directors;
        }
        public async Task<Director> ReadDirectorAsync(string name, bool include)
        {
            Director director;
            name = name.ToUpper();

            if (include)
            {
                director = await _context.Directors.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                director = FilterDirector(director);
            }
            else
            {
                director = await _context.Directors.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return director;
        }
        public async Task<bool> ReadDirectorAsync(Guid id)
        {
            Director director = await ReadDirectorAsync(id, false);

            return director == null ? false : true;
        }


        // DownloadLink - All, Id, Title
        public async Task<List<DownloadLink>> ReadDownloadLinkAsync(bool include, int noOfRecords = 0)
        {
            List<DownloadLink> downloadLinks;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    downloadLinks = await _context.DownloadLinks.Include(x => x.Movie).OrderBy(x => x.Title).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    downloadLinks = await _context.DownloadLinks.Include(x => x.Movie).OrderBy(x => x.Title).ToListAsync();
                }

                downloadLinks = FilterDownloadLink(downloadLinks);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    downloadLinks = await _context.DownloadLinks.OrderBy(x => x.Title).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    downloadLinks = await _context.DownloadLinks.OrderBy(x => x.Title).ToListAsync();
                }
            }

            return downloadLinks;
        }
        public async Task<DownloadLink> ReadDownloadLinkAsync(Guid id, bool include)
        {
            DownloadLink downloadLink;

            if (include)
            {
                downloadLink = await _context.DownloadLinks.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Id == id);
                downloadLink = FilterDownloadLink(downloadLink);
            }
            else
            {
                downloadLink = await _context.DownloadLinks.FirstOrDefaultAsync(x => x.Id == id);
            }

            return downloadLink;
        }
        public async Task<List<DownloadLink>> ReadDownloadLinkAsync(string title, bool include, int noOfRecords = 0)
        {
            List<DownloadLink> downloadLinks;
            title = title.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    downloadLinks = await _context.DownloadLinks.Include(x => x.Movie).OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    downloadLinks = await _context.DownloadLinks.Include(x => x.Movie).OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }

                downloadLinks = FilterDownloadLink(downloadLinks);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    downloadLinks = await _context.DownloadLinks.OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    downloadLinks = await _context.DownloadLinks.OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }
            }

            return downloadLinks;
        }
        public async Task<DownloadLink> ReadDownloadLinkAsync(string title, bool include)
        {
            DownloadLink downloadLink;
            title = title.ToUpper();

            if (include)
            {
                downloadLink = await _context.DownloadLinks.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
                downloadLink = FilterDownloadLink(downloadLink);
            }
            else
            {
                downloadLink = await _context.DownloadLinks.FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
            }

            return downloadLink;
        }
        public async Task<bool> ReadDownloadLinkAsync(Guid id)
        {
            DownloadLink downloadLink = await ReadDownloadLinkAsync(id, false);

            return downloadLink == null ? false : true;
        }


        // FilmStar - All, Id, Name
        public async Task<List<FilmStar>> ReadFilmStarAsync(bool include, int noOfRecords = 0)
        {
            List<FilmStar> filmStars;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    filmStars = await _context.FilmStars.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    filmStars = await _context.FilmStars.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                filmStars = FilterFilmStar(filmStars);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    filmStars = await _context.FilmStars.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    filmStars = await _context.FilmStars.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return filmStars;
        }
        public async Task<FilmStar> ReadFilmStarAsync(Guid id, bool include)
        {
            FilmStar filmStar;

            if (include)
            {
                filmStar = await _context.FilmStars.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                filmStar = FilterFilmStar(filmStar);
            }
            else
            {
                filmStar = await _context.FilmStars.FirstOrDefaultAsync(x => x.Id == id);
            }

            return filmStar;
        }
        public async Task<List<FilmStar>> ReadFilmStarAsync(string name, bool include, int noOfRecords = 0)
        {
            List<FilmStar> filmStars;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    filmStars = await _context.FilmStars.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    filmStars = await _context.FilmStars.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                filmStars = FilterFilmStar(filmStars);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    filmStars = await _context.FilmStars.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    filmStars = await _context.FilmStars.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return filmStars;
        }
        public async Task<FilmStar> ReadFilmStarAsync(string name, bool include)
        {
            FilmStar filmStar;
            name = name.ToUpper();

            if (include)
            {
                filmStar = await _context.FilmStars.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                filmStar = FilterFilmStar(filmStar);
            }
            else
            {
                filmStar = await _context.FilmStars.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return filmStar;
        }
        public async Task<bool> ReadFilmStarAsync(Guid id)
        {
            FilmStar filmStar = await ReadFilmStarAsync(id, false);

            return filmStar == null ? false : true;
        }


        // Language - All, Id, Name
        public async Task<List<Language>> ReadLanguageAsync(bool include, int noOfRecords = 0)
        {
            List<Language> languages;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    languages = await _context.Languages.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    languages = await _context.Languages.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                languages = FilterLanguage(languages);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    languages = await _context.Languages.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    languages = await _context.Languages.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return languages;
        }
        public async Task<Language> ReadLanguageAsync(Guid id, bool include)
        {
            Language language;

            if (include)
            {
                language = await _context.Languages.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                language = FilterLanguage(language);
            }
            else
            {
                language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            }

            return language;
        }
        public async Task<List<Language>> ReadLanguageAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Language> languages;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    languages = await _context.Languages.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    languages = await _context.Languages.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                languages = FilterLanguage(languages);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    languages = await _context.Languages.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    languages = await _context.Languages.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return languages;
        }
        public async Task<Language> ReadLanguageAsync(string name, bool include)
        {
            Language language;
            name = name.ToUpper();

            if (include)
            {
                language = await _context.Languages.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                language = FilterLanguage(language);
            }
            else
            {
                language = await _context.Languages.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return language;
        }
        public async Task<bool> ReadLanguageAsync(Guid id)
        {
            Language language = await ReadLanguageAsync(id, false);

            return language == null ? false : true;
        }


        // Movie - All, Id, Title
        public async Task<List<Movie>> ReadMovieAsync(bool include, int noOfRecords = 0)
        {
            List<Movie> movies;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    movies = await _context.Movies
                        .Include(x => x.Country)
                        .Include(x => x.Director)
                        .Include(x => x.Quality)
                        .Include(x => x.Languages)
                        .Include(x => x.FilmStars)
                        .Include(x => x.Categories)
                        .Include(x => x.Screenshots)
                        .Include(x => x.DownloadLinks)
                        .OrderByDescending(x => x.DateOfUpload).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    movies = await _context.Movies
                         .Include(x => x.Country)
                         .Include(x => x.Director)
                         .Include(x => x.Quality)
                         .Include(x => x.Languages)
                         .Include(x => x.FilmStars)
                         .Include(x => x.Categories)
                         .Include(x => x.Screenshots)
                         .Include(x => x.DownloadLinks)
                         .OrderByDescending(x => x.DateOfUpload).ToListAsync();
                }

                movies = FilterMovie(movies);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    movies = await _context.Movies.OrderByDescending(x => x.DateOfUpload).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    movies = await _context.Movies.OrderByDescending(x => x.DateOfUpload).ToListAsync();
                }
            }

            return movies;
        }
        public async Task<Movie> ReadMovieAsync(Guid id, bool include)
        {
            Movie movie;

            if (include)
            {
                movie = await _context.Movies
                    .Include(x => x.Country)
                        .Include(x => x.Director)
                        .Include(x => x.Quality)
                        .Include(x => x.Languages)
                        .Include(x => x.FilmStars)
                        .Include(x => x.Categories)
                        .Include(x => x.Screenshots)
                        .Include(x => x.DownloadLinks)
                    .FirstOrDefaultAsync(x => x.Id == id);

                movie = FilterMovie(movie);
            }
            else
            {
                movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            }

            return movie;
        }
        public async Task<List<Movie>> ReadMovieAsync(string title, bool include, int noOfRecords = 0)
        {
            List<Movie> movies;
            title = title.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    movies = await _context.Movies
                        .Include(x => x.Country)
                        .Include(x => x.Director)
                        .Include(x => x.Quality)
                        .Include(x => x.Languages)
                        .Include(x => x.FilmStars)
                        .Include(x => x.Categories)
                        .Include(x => x.Screenshots)
                        .Include(x => x.DownloadLinks)
                        .OrderByDescending(x => x.DateOfUpload).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    movies = await _context.Movies
                         .Include(x => x.Country)
                         .Include(x => x.Director)
                         .Include(x => x.Quality)
                         .Include(x => x.Languages)
                         .Include(x => x.FilmStars)
                         .Include(x => x.Categories)
                         .Include(x => x.Screenshots)
                         .Include(x => x.DownloadLinks)
                         .OrderByDescending(x => x.DateOfUpload).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }

                movies = FilterMovie(movies);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    movies = await _context.Movies.OrderByDescending(x => x.DateOfUpload).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    movies = await _context.Movies.OrderByDescending(x => x.DateOfUpload).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }
            }

            return movies;
        }
        public async Task<Movie> ReadMovieAsync(string title, bool include)
        {
            Movie movie;
            title = title.ToUpper();

            if (include)
            {
                movie = await _context.Movies
                    .Include(x => x.Country)
                        .Include(x => x.Director)
                        .Include(x => x.Quality)
                        .Include(x => x.Languages)
                        .Include(x => x.FilmStars)
                        .Include(x => x.Categories)
                        .Include(x => x.Screenshots)
                        .Include(x => x.DownloadLinks)
                    .FirstOrDefaultAsync(x => x.Title.ToUpper() == title);

                movie = FilterMovie(movie);
            }
            else
            {
                movie = await _context.Movies.FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
            }

            return movie;
        }
        public async Task<bool> ReadMovieAsync(Guid id)
        {
            Movie movie = await ReadMovieAsync(id, false);

            return movie == null ? false : true;
        }


        // Quality - All, Id, Name
        public async Task<List<Quality>> ReadQualityAsync(bool include, int noOfRecords = 0)
        {
            List<Quality> qualities;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    qualities = await _context.Qualities.Include(x => x.Movies).OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    qualities = await _context.Qualities.Include(x => x.Movies).OrderBy(x => x.Name).ToListAsync();
                }

                qualities = FilterQuality(qualities);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    qualities = await _context.Qualities.OrderBy(x => x.Name).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    qualities = await _context.Qualities.OrderBy(x => x.Name).ToListAsync();
                }
            }

            return qualities;
        }
        public async Task<Quality> ReadQualityAsync(Guid id, bool include)
        {
            Quality quality;

            if (include)
            {
                quality = await _context.Qualities.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
                quality = FilterQuality(quality);
            }
            else
            {
                quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Id == id);
            }

            return quality;
        }
        public async Task<List<Quality>> ReadQualityAsync(string name, bool include, int noOfRecords = 0)
        {
            List<Quality> qualities;
            name = name.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    qualities = await _context.Qualities.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    qualities = await _context.Qualities.Include(x => x.Movies).OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }

                qualities = FilterQuality(qualities);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    qualities = await _context.Qualities.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    qualities = await _context.Qualities.OrderBy(x => x.Name).Where(x => x.Name.ToUpper().StartsWith(name)).ToListAsync();
                }
            }

            return qualities;
        }
        public async Task<Quality> ReadQualityAsync(string name, bool include)
        {
            Quality quality;
            name = name.ToUpper();

            if (include)
            {
                quality = await _context.Qualities.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
                quality = FilterQuality(quality);
            }
            else
            {
                quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
            }

            return quality;
        }
        public async Task<bool> ReadQualityAsync(Guid id)
        {
            Quality quality = await ReadQualityAsync(id, false);

            return quality == null ? false : true;
        }


        // Screenshot - All, Id, Title
        public async Task<List<Screenshot>> ReadScreenshotAsync(bool include, int noOfRecords = 0)
        {
            List<Screenshot> screenshots;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    screenshots = await _context.Screenshots.Include(x => x.Movie).OrderBy(x => x.Title).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    screenshots = await _context.Screenshots.Include(x => x.Movie).OrderBy(x => x.Title).ToListAsync();
                }

                screenshots = FilterScreenshot(screenshots);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    screenshots = await _context.Screenshots.OrderBy(x => x.Title).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    screenshots = await _context.Screenshots.OrderBy(x => x.Title).ToListAsync();
                }
            }

            return screenshots;
        }
        public async Task<Screenshot> ReadScreenshotAsync(Guid id, bool include)
        {
            Screenshot screenshot;

            if (include)
            {
                screenshot = await _context.Screenshots.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Id == id);
                screenshot = FilterScreenshot(screenshot);
            }
            else
            {
                screenshot = await _context.Screenshots.FirstOrDefaultAsync(x => x.Id == id);
            }

            return screenshot;
        }
        public async Task<List<Screenshot>> ReadScreenshotAsync(string title, bool include, int noOfRecords = 0)
        {
            List<Screenshot> screenshots;
            title = title.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    screenshots = await _context.Screenshots.Include(x => x.Movie).OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    screenshots = await _context.Screenshots.Include(x => x.Movie).OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }

                screenshots = FilterScreenshot(screenshots);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    screenshots = await _context.Screenshots.OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    screenshots = await _context.Screenshots.OrderBy(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }
            }

            return screenshots;
        }
        public async Task<Screenshot> ReadScreenshotAsync(string title, bool include)
        {
            Screenshot screenshot;
            title = title.ToUpper();

            if (include)
            {
                screenshot = await _context.Screenshots.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
                screenshot = FilterScreenshot(screenshot);
            }
            else
            {
                screenshot = await _context.Screenshots.FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
            }

            return screenshot;
        }
        public async Task<bool> ReadScreenshotAsync(Guid id)
        {
            Screenshot screenshot = await ReadScreenshotAsync(id, false);

            return screenshot == null ? false : true;
        }


        // SlideShow - All, Id, Title
        public async Task<List<SlideShow>> ReadSlideShowAsync(bool include, int noOfRecords = 0)
        {
            List<SlideShow> slideShows;

            if (include)
            {
                if (noOfRecords > 0)
                {
                    slideShows = await _context.SlideShows.Include(x => x.Movie).OrderByDescending(x => x.Order).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    slideShows = await _context.SlideShows.Include(x => x.Movie).OrderByDescending(x => x.Order).ToListAsync();
                }

                slideShows = FilterSlideShow(slideShows);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    slideShows = await _context.SlideShows.OrderByDescending(x => x.Order).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    slideShows = await _context.SlideShows.OrderByDescending(x => x.Order).ToListAsync();
                }
            }

            return slideShows;
        }
        public async Task<SlideShow> ReadSlideShowAsync(Guid id, bool include)
        {
            SlideShow slideShow;

            if (include)
            {
                slideShow = await _context.SlideShows.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Id == id);
                slideShow = FilterSlideShow(slideShow);
            }
            else
            {
                slideShow = await _context.SlideShows.FirstOrDefaultAsync(x => x.Id == id);
            }

            return slideShow;
        }
        public async Task<List<SlideShow>> ReadSlideShowAsync(string title, bool include, int noOfRecords = 0)
        {
            List<SlideShow> slideShows;
            title = title.ToUpper();

            if (include)
            {
                if (noOfRecords > 0)
                {
                    slideShows = await _context.SlideShows.Include(x => x.Movie).OrderByDescending(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    slideShows = await _context.SlideShows.Include(x => x.Movie).OrderByDescending(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }

                slideShows = FilterSlideShow(slideShows);
            }
            else
            {
                if (noOfRecords > 0)
                {
                    slideShows = await _context.SlideShows.OrderByDescending(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).Take(noOfRecords).ToListAsync();
                }
                else
                {
                    slideShows = await _context.SlideShows.OrderByDescending(x => x.Title).Where(x => x.Title.ToUpper().StartsWith(title)).ToListAsync();
                }
            }

            return slideShows;
        }
        public async Task<SlideShow> ReadSlideShowAsync(string title, bool include)
        {
            SlideShow slideShow;
            title = title.ToUpper();

            if (include)
            {
                slideShow = await _context.SlideShows.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
                slideShow = FilterSlideShow(slideShow);
            }
            else
            {
                slideShow = await _context.SlideShows.FirstOrDefaultAsync(x => x.Title.ToUpper() == title);
            }

            return slideShow;
        }
        public async Task<bool> ReadSlideShowAsync(Guid id)
        {
            SlideShow slideShow = await ReadSlideShowAsync(id, false);

            return slideShow == null ? false : true;
        }
    }

    // Update
    public partial class MoviesGalleryRepository
    {
        // Category
        public async Task<string> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            _context.Entry(category).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? category.Id.ToString() : null;
        }

        // Country
        public async Task<string> UpdateCountryAsync(Country country)
        {
            _context.Countries.Update(country);
            _context.Entry(country).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? country.Id.ToString() : null;
        }

        // Director
        public async Task<string> UpdateDirectorAsync(Director director)
        {
            _context.Directors.Update(director);
            _context.Entry(director).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? director.Id.ToString() : null;
        }

        // DownloadLink
        public async Task<string> UpdateDownloadLinkAsync(DownloadLink downloadLink)
        {
            _context.DownloadLinks.Update(downloadLink);
            _context.Entry(downloadLink).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? downloadLink.Id.ToString() : null;
        }

        // FilmStar
        public async Task<string> UpdateFilmStarAsync(FilmStar filmStar)
        {
            _context.FilmStars.Update(filmStar);
            _context.Entry(filmStar).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? filmStar.Id.ToString() : null;
        }

        // Language
        public async Task<string> UpdateLanguageAsync(Language language)
        {
            _context.Languages.Update(language);
            _context.Entry(language).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? language.Id.ToString() : null;
        }

        // Movie
        public async Task<string> UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.Entry(movie).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? movie.Id.ToString() : null;
        }

        // Quality
        public async Task<string> UpdateQualityAsync(Quality quality)
        {
            _context.Qualities.Update(quality);
            _context.Entry(quality).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? quality.Id.ToString() : null;
        }

        // Screenshot
        public async Task<string> UpdateScreenshotAsync(Screenshot screenshot)
        {
            _context.Screenshots.Update(screenshot);
            _context.Entry(screenshot).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? screenshot.Id.ToString() : null;
        }

        // SlideShow
        public async Task<string> UpdateSlideShowAsync(SlideShow slideShow)
        {
            _context.SlideShows.Update(slideShow);
            _context.Entry(slideShow).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? slideShow.Id.ToString() : null;
        }
    }

    // Delete
    public partial class MoviesGalleryRepository
    {
        // Category
        public async Task<string> DeleteCategoryAsync(Guid id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(category);
            _context.Entry(category).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? category.Id.ToString() : null;
        }

        // Country
        public async Task<string> DeleteCountryAsync(Guid id)
        {
            int result;
            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            _context.Countries.Remove(country);
            _context.Entry(country).State = EntityState.Deleted;
            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return $"The country you were trying to delete is used in movie(s), you need to update or delete movie(s) for deleting this country with Id : {id}";
            }

            return result > 0 ? country.Id.ToString() : null;
        }

        // Director
        public async Task<string> DeleteDirectorAsync(Guid id)
        {
            int result;
            Director director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == id);
            _context.Directors.Remove(director);
            _context.Entry(director).State = EntityState.Deleted;
            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return $"Director you were trying to delete is used in movie(s), you need to update or delete movie(s) for deleting this director with Id : {id}";
            }

            return result > 0 ? director.Id.ToString() : null;
        }

        // DownloadLink
        public async Task<string> DeleteDownloadLinkAsync(Guid id)
        {
            DownloadLink downloadLink = await _context.DownloadLinks.FirstOrDefaultAsync(x => x.Id == id);
            _context.DownloadLinks.Remove(downloadLink);
            _context.Entry(downloadLink).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? downloadLink.Id.ToString() : null;
        }

        // FilmStar
        public async Task<string> DeleteFilmStarAsync(Guid id)
        {
            FilmStar filmStar = await _context.FilmStars.FirstOrDefaultAsync(x => x.Id == id);
            _context.FilmStars.Remove(filmStar);
            _context.Entry(filmStar).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? filmStar.Id.ToString() : null;
        }

        // Language
        public async Task<string> DeleteLanguageAsync(Guid id)
        {
            Language language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            _context.Languages.Remove(language);
            _context.Entry(language).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? language.Id.ToString() : null;
        }

        // Movie
        public async Task<Movie> DeleteMovieAsync(Guid id)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            _context.Movies.Remove(movie);
            _context.Entry(movie).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? movie : null;
        }

        // Quality
        public async Task<string> DeleteQualityAsync(Guid id)
        {
            int result;
            Quality quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Id == id);
            _context.Qualities.Remove(quality);
            _context.Entry(quality).State = EntityState.Deleted;
            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return $"Quality you were trying to delete is used in movie(s), you need to update or delete movie(s) for deleting this quality with Id : {id}";
            }

            return result > 0 ? quality.Id.ToString() : null;
        }

        // Screenshot
        public async Task<Screenshot> DeleteScreenshotAsync(Guid id)
        {
            Screenshot screenshot = await _context.Screenshots.FirstOrDefaultAsync(x => x.Id == id);
            _context.Screenshots.Remove(screenshot);
            _context.Entry(screenshot).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? screenshot : null;
        }

        // SlideShow
        public async Task<SlideShow> DeleteSlideShowAsync(Guid id)
        {
            SlideShow slideShow = await _context.SlideShows.FirstOrDefaultAsync(x => x.Id == id);
            _context.SlideShows.Remove(slideShow);
            _context.Entry(slideShow).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result > 0 ? slideShow : null;
        }
    }

    // Filters 
    public partial class MoviesGalleryRepository
    {
        // Common
        private List<Movie> FilterCommon(List<Movie> movies)
        {
            List<Movie> result = new List<Movie>();

            foreach (Movie movie in movies)
            {
                result.Add(FilterCommon(movie));
            }

            return result;
        }
        private Movie FilterCommon(Movie movie)
        {
            movie.Country = null;
            movie.Director = null;
            movie.Quality = null;
            movie.Languages = null;
            movie.FilmStars = null;
            movie.Categories = null;
            movie.Screenshots = null;
            movie.DownloadLinks = null;

            return movie;
        }

        // Category
        private List<Category> FilterCategory(List<Category> categories)
        {
            foreach (Category category in categories)
            {
                category.Movies = FilterCommon(category.Movies);
            }

            return categories;
        }
        private Category FilterCategory(Category category)
        {
            if (category != null)
            {
                category.Movies = FilterCommon(category.Movies);
            }

            return category;
        }

        // Country
        private List<Country> FilterCountry(List<Country> countries)
        {
            foreach (Country country in countries)
            {
                country.Movies = FilterCommon(country.Movies);
            }

            return countries;
        }
        private Country FilterCountry(Country country)
        {
            if (country != null)
            {
                country.Movies = FilterCommon(country.Movies);
            }

            return country;
        }

        // Director
        private List<Director> FilterDirector(List<Director> directors)
        {
            foreach (Director director in directors)
            {
                director.Movies = FilterCommon(director.Movies);
            }

            return directors;
        }
        private Director FilterDirector(Director director)
        {
            if (director != null)
            {
                director.Movies = FilterCommon(director.Movies);
            }

            return director;
        }

        // DownloadLink
        private List<DownloadLink> FilterDownloadLink(List<DownloadLink> downloadLinks)
        {
            foreach (DownloadLink downloadLink in downloadLinks)
            {
                downloadLink.Movie = FilterCommon(downloadLink.Movie);
            }

            return downloadLinks;
        }
        private DownloadLink FilterDownloadLink(DownloadLink downloadLink)
        {
            if (downloadLink != null)
            {
                downloadLink.Movie = FilterCommon(downloadLink.Movie);
            }

            return downloadLink;
        }

        // FilmStar
        private List<FilmStar> FilterFilmStar(List<FilmStar> filmStars)
        {
            foreach (FilmStar filmStar in filmStars)
            {
                filmStar.Movies = FilterCommon(filmStar.Movies);
            }

            return filmStars;
        }
        private FilmStar FilterFilmStar(FilmStar filmStar)
        {
            if (filmStar != null)
            {
                filmStar.Movies = FilterCommon(filmStar.Movies);
            }

            return filmStar;
        }

        // Language
        private List<Language> FilterLanguage(List<Language> languages)
        {
            foreach (Language language in languages)
            {
                language.Movies = FilterCommon(language.Movies);
            }

            return languages;
        }
        private Language FilterLanguage(Language language)
        {
            if (language != null)
            {
                language.Movies = FilterCommon(language.Movies);
            }

            return language;
        }

        // Movie
        private List<Movie> FilterMovie(List<Movie> movies)
        {
            List<Movie> result = new List<Movie>();

            foreach (Movie movie in movies)
            {
                result.Add(FilterMovie(movie));
            }

            return result;
        }
        private Movie FilterMovie(Movie movie)
        {
            if (movie != null)
            {
                if (movie.Country != null)
                {
                    movie.Country.Movies = null;
                }
                if (movie.Director != null)
                {
                    movie.Director.Movies = null;
                }
                if (movie.Quality != null)
                {
                    movie.Quality.Movies = null;
                }
                foreach (Category category in movie.Categories)
                {
                    category.Movies = null;
                }
                foreach (DownloadLink downloadLink in movie.DownloadLinks)
                {
                    downloadLink.Movie = null;
                }
                foreach (FilmStar filmStar in movie.FilmStars)
                {
                    filmStar.Movies = null;
                }
                foreach (Language language in movie.Languages)
                {
                    language.Movies = null;
                }
                foreach (Screenshot screenshot in movie.Screenshots)
                {
                    screenshot.Movie = null;
                }
            }

            return movie;
        }

        // Quality
        private List<Quality> FilterQuality(List<Quality> qualities)
        {
            foreach (Quality quality in qualities)
            {
                quality.Movies = FilterCommon(quality.Movies);
            }

            return qualities;
        }
        private Quality FilterQuality(Quality quality)
        {
            if (quality != null)
            {
                quality.Movies = FilterCommon(quality.Movies);
            }

            return quality;
        }

        // Screenshot
        private List<Screenshot> FilterScreenshot(List<Screenshot> screenshots)
        {
            foreach (Screenshot screenshot in screenshots)
            {
                screenshot.Movie = FilterCommon(screenshot.Movie);
            }

            return screenshots;
        }
        private Screenshot FilterScreenshot(Screenshot screenshot)
        {
            if (screenshot != null)
            {
                screenshot.Movie = FilterCommon(screenshot.Movie);
            }

            return screenshot;
        }

        // SlideShow
        private List<SlideShow> FilterSlideShow(List<SlideShow> slideShows)
        {
            foreach (SlideShow slideShow in slideShows)
            {
                slideShow.Movie = FilterCommon(slideShow.Movie);
            }

            return slideShows;
        }
        private SlideShow FilterSlideShow(SlideShow slideShow)
        {
            if (slideShow != null)
            {
                slideShow.Movie = FilterCommon(slideShow.Movie);
            }

            return slideShow;
        }
    }
}

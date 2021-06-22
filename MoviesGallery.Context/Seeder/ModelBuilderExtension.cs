using Microsoft.EntityFrameworkCore;
using MoviesGallery.Models;
using System;
using System.Collections.Generic;

namespace MoviesGallery.Context.Seeder
{
    public static class ModelBuilderExtension
    {
        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            List<Category> categories = new List<Category>();

            for (int i = 0; i < 10; i++)
            {
                categories.Add(new Category
                {
                    Id = Guid.NewGuid(),
                    Name = $"Category {i + 1}"
                });
            }

            modelBuilder.Entity<Category>().HasData(categories);
        }
        public static void SeedCountries(this ModelBuilder modelBuilder)
        {
            string[] allCountries = { "Canada", "Brazil", "Argentina", "Russia", "Italy", "Spain", "Germany", "South Korea", "France", "UK", "Japan", "China", "USA", "Nigeria", "India" };

            List<Country> countries = new List<Country>();

            for (int i = 0; i < allCountries.Length; i++)
            {
                countries.Add(new Country
                {
                    Id = Guid.NewGuid(),
                    Name = allCountries[i]
                });
            }

            modelBuilder.Entity<Country>().HasData(countries);
        }
        public static void SeedDirectors(this ModelBuilder modelBuilder)
        {
            List<Director> directors = new List<Director>();

            for (int i = 0; i < 10; i++)
            {
                directors.Add(new Director
                {
                    Id = Guid.NewGuid(),
                    Name = $"Director {i + 1}"
                });
            }

            modelBuilder.Entity<Director>().HasData(directors);
        }
        public static void SeedFilmStars(this ModelBuilder modelBuilder)
        {
            List<FilmStar> filmStars = new List<FilmStar>();

            for (int i = 0; i < 10; i++)
            {
                filmStars.Add(new FilmStar
                {
                    Id = Guid.NewGuid(),
                    Name = $"FilmStar {i + 1}"
                });
            }

            modelBuilder.Entity<FilmStar>().HasData(filmStars);
        }
        public static void SeedLanguages(this ModelBuilder modelBuilder)
        {
            string[] allLanguages = { "French", "Spanish", "German", "Hindi", "English", "Mandarin", "Japanese", "Italian", "Russian", "Arabic", "Korean", "Hebrew", "Cantonese", "Portuguese", "Swedish", "Latin", "Ukrainian", "Danish", "Persian"};

            List<Language> languages = new List<Language>();

            for (int i = 0; i < allLanguages.Length; i++)
            {
                languages.Add(new Language
                {
                    Id = Guid.NewGuid(),
                    Name = allLanguages[i]
                });
            }

            modelBuilder.Entity<Language>().HasData(languages);
        }
        public static void SeedMovies(this ModelBuilder modelBuilder)
        {
            List<Movie> movies = new List<Movie>();

            for (int i = 0; i < 10; i++)
            {
                movies.Add(new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = $"Movie {i + 1}"
                });
            }

            modelBuilder.Entity<Movie>().HasData(movies);
        }
        public static void SeedQualities(this ModelBuilder modelBuilder)
        {
            List<Quality> qualities = new List<Quality>();

            for (int i = 0; i < 10; i++)
            {
                qualities.Add(new Quality
                {
                    Id = Guid.NewGuid(),
                    Name = $"Quality {i + 1}"
                });
            }

            modelBuilder.Entity<Quality>().HasData(qualities);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedCategories(modelBuilder);
            SeedCountries(modelBuilder);
            SeedDirectors(modelBuilder);
            SeedFilmStars(modelBuilder);
            SeedLanguages(modelBuilder);
            SeedMovies(modelBuilder);
            SeedQualities(modelBuilder);
        }
    }
}

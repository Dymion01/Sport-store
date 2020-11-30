using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            if (context.Products.Any()) return;
            context.Products.AddRange(
                new Product
                {
                    Name = "Kajak",
                    Description = "Łódka przeznaczona dla jednej osoby",
                    CategoryId = 1,
                    Price = 275
                },
                new Product
                {
                    Name = "Kamizelka ratunkowa",
                    Description = "Chroni i dodaje uroku",
                    CategoryId = 1,
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Piłka",
                    Description = "Zatwierdzone przez FIFA rozmiar i waga",
                    CategoryId = 2, //piłka nożna
                    Price = 19.50m
                },
                new Product
                {
                    Name = "Flagi narożne",
                    Description = "Nadadzą twojemu boisku profesjonalny wygląd",
                    CategoryId = 2,
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadiom",
                    Description = "Składany stadion na 35 000 osób",
                    CategoryId = 2,
                    Price = 79500
                },
                new Product
                {
                    Name = "Czapka",
                    Description = "Zwiększa efektywność mózgu o 75%",
                    CategoryId = 3,
                    Price = 16
                },
                new Product
                {
                    Name = "Niestabilne krzesło",
                    Description = "Zmniejsza szanse przeciwnika",
                    CategoryId = 3,
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Ludzka szachownica",
                    Description = "Przyjemna gra dla całej rodziny!",
                    CategoryId = 3,
                    Price = 75
                },
                new Product
                {
                    Name = "Błyszczący król",
                    Description = "Figura pokryta złotem i wysadzana diamentami",
                    CategoryId = 3,
                    Price = 1200
                }
            );
            context.SaveChanges();
        }

    }
}

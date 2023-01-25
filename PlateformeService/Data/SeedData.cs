using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlateformeService.Models;
using System;
using System.Linq;

namespace PlateformeService.Data
{
    public static class AppExtensions
    {
        public static void SeedData(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
                CreateData(scope.ServiceProvider.GetService<AppDbContext>(), env);
        }


        private static void CreateData(AppDbContext context, IWebHostEnvironment env )
        {
            if (env.IsProduction())
            {
                context.Database.Migrate();
            }

            if (context.Plateforms.Any())
                return;

            Console.WriteLine("Seeding data...");

            context.Plateforms.AddRange(
                new Plateform
                {
                    Name = "Sql Server",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },

                new Plateform
                {
                    Name = "Docker",
                    Publisher = "Docker",
                    Cost = "Free"
                },
                new Plateform
                {
                    Name = "Dotnet",
                    Publisher = "Microsoft",
                    Cost = "Free"
                }
                );
            context.SaveChanges();
        }
    }
}

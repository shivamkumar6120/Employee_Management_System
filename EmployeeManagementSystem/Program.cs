using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //This line creates a WebApplicationBuilder instance with preconfigured defaults, 
            //such as setting up the internal Kestrel web server, reading configuration files
            //(appsettings.json), and configuring logging.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //One repository instance per request --- Best for DB access
            builder.Services.AddScoped<EmployeeManagementSystem.Data.EmployeeRepository>();


            var app = builder.Build(); //builds the final WebApplication instance

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

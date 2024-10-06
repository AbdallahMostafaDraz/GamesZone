using GameZone.EF.Data;
using GameZone.EF.Services.Implementions;
using GameZone.EF.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add dbcontext
            builder.Services.AddDbContext<AppDBContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException()));
            
            // Add unitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

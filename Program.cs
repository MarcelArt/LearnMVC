using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LearnMVC.Data;
using Microsoft.AspNetCore.Identity;
using LearnMVC.Areas.Identity.Data;
namespace LearnMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<LearnMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LearnMVCContext") ?? throw new InvalidOperationException("Connection string 'LearnMVCContext' not found.")));
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LearnMVCContext") ?? throw new InvalidOperationException("Connection string 'LearnMVCContext' not found.")));

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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
            app.MapRazorPages();

            app.Run();
        }
    }
}

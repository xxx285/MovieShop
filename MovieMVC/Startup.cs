using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using MovieShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Registering our classes for interrfaces to be used across our application
            //.NET CORE built-int Dependency Injection
            //.NET Framework does not havev built-in DI, will need download 3rd party packages or IOC, like Ninect, Autofac... 
            services.AddTransient<IMovieService, MovieService>(); // whenever we see IMovieService as a constructor parameter, will replace that with MovieService Class; change here if we want to pass a new class as parameters
            services.AddTransient<IMovieRepository, MovieRepository>();
            
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IAsyncRepository<Genre>, EfRepository<Genre>>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<ICurrentLogedInUser, CurrentLogedInUser>();

            services.AddDbContext<MovieShopDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                option =>
                {
                    option.Cookie.Name = "MoviShopAuthCookie";
                    option.ExpireTimeSpan = TimeSpan.FromHours(2);
                    option.LoginPath = "/Account/login";
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // this is important for setting up cookie
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

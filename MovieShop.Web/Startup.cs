using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

namespace MovieShop.Web
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

            // .NET Core has built-in IOC support  but .NET Framework didn't have one using thrid party lib 

            services.AddDbContext<MovieShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(("MovieShopDbConnection"))));

            // Registering our DI services Binding  services 
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();
            
            
            //  class
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //
            services.AddScoped<IAsyncRepository<MovieGenre>, EfRepository<MovieGenre>>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IAsyncRepository<Favorite>, EfRepository<Favorite>>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAsyncRepository<Purchase>, EfRepository<Purchase>>();
            services.AddScoped<IAsyncRepository<Review>, EfRepository<Review>>();

            //
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            //sets the default authentication scheme for the app

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "MovieShopAuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/Account/Login";
            });

           

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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Traditional based routing / Conventional based routing
                //  http://example.com/Students/Index ==> GET
                //  http://example.com/ ==> GET
                //  http://example.com/Movies ==> GET
                //  http://example.com/Movies/Create ==> GET
                //  http://example.com/Movies/Details/22 ==> GET

                //  http://example.com/Students/List/2019/December
            });
        }
    }
}

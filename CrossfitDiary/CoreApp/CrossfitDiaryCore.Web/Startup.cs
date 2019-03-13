using System;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.BL.Services.DapperStuff;
using CrossfitDiaryCore.BL.Services.WorkoutMatchers;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CrossfitDiaryCore.Web
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
            string connectionString = Configuration.GetConnectionString("CrossfitDiaryDB_Core");
            services.AddTransient<RoutineComplexRepository>(provider => new RoutineComplexRepository(connectionString));

            services.AddAutoMapper();
            services.AddMemoryCache();

            services.AddMvc().AddControllersAsServices();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//            services.AddA(_serviceProvider.GetService<IHttpContextAccessor>());
            //
            services.AddDbContext<WorkouterContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WorkouterContext>()
                .AddDefaultTokenProviders();
            //
            services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["oAuthConfiguration:google:clientId"];
                googleOptions.ClientSecret = Configuration["oAuthConfiguration:google:clientSecret"];
            })
            .AddVkontakte(options =>
            {
                options.ClientId = Configuration["oAuthConfiguration:vkontakte:clientId"];
                options.ClientSecret = Configuration["oAuthConfiguration:vkontakte:clientSecret"];
                options.Scope.Add("email");
            });

            services.Configure<IdentityOptions>(
                options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 6;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                });

            services.ConfigureApplicationCookie(
                options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Expiration = TimeSpan.FromDays(150);
                    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                    options.SlidingExpiration = true;
                });

            services.AddTransient<ReadWorkoutsService>();
            services.AddTransient<ReadUsersService>();
            services.AddTransient<ReadExercisesService>();
            services.AddTransient<ManageWorkoutsService>();
            services.AddTransient<ManagerUsersService>();

            services.AddTransient<WorkoutsMatchDispatcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
//                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
//            app.UseStatusCodePagesWithRedirects("/Error");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Persons}/{action=Index}/{id?}");
            });
        }
    }
}

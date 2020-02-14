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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                // TODO: Use your User Agent library of choice here. 
                if (DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }

        public static bool DisallowsSameSiteNone(string userAgent)
        {
            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS networking
            // stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            string connectionString = Configuration.GetConnectionString("CrossfitDiaryDB_Core");
            services.AddTransient(provider => new DapperRepository(connectionString));

            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();

            services.AddMvc()
                .AddControllersAsServices()
                .AddNewtonsoftJson();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//            services.AddA(_serviceProvider.GetService<IHttpContextAccessor>());
            //
            services.AddDbContext<WorkouterContext>(options => options.UseSqlServer(connectionString));

            // services.AddDefaultIdentity<ApplicationUser>()
                // .AddEntityFrameworkStores<WorkouterContext>();

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
            
                    // options.Cookie.HttpOnly = true; 
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
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
            services.AddTransient<MaximumsUpdater>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            //            app.UseStatusCodePagesWithRedirects("/Error");
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(name: "default", "{controller=Persons}/{action=Index}/{id?}");
            });
        }
    }
}

namespace SimonSampleApp.Web
{
    using System;
    using System.Net.Http.Headers;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Services.OneSignal;

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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IOneSignalService, OneSignalService>();
            services.AddHttpClient<IOneSignalService, OneSignalService>(c =>
            {
                c.BaseAddress = new Uri("https://onesignal.com/api/v1/");
                //todo:serj: Read from settings
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YzgwNDE1NTAtMWE2NC00MTJkLThhOGYtNGYxMTVjMDVkMmVh");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
            
            try
            {
                // Migrate and seed ApplicationDbContext (don't use the IOC because of single usage)
                new AppDbContextSeedingService(serviceScope.ServiceProvider.GetRequiredService<AppDbContext>(),
                        serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>(),
                        serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                    .EnsureMigratedAndSeededAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate or seed database");
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
using FluentValidation;
using Lawyer.CRM.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManager.EntityFramework;
using ProjectManager.Repository;
using ProjectManager.Services;
using ProjectManager.Services.Developer;
using ProjectManager.Services.Link;
using ProjectManager.Services.Project;
using ProjectManager.Web.Models;
using ProjectManager.Web.Models.Developers;
using ProjectManager.Web.Models.Projects;
using static ProjectManager.Web.Models.LinkDevToProjectHttpPostModel;
using static ProjectManager.Web.Models.Projects.UpdateProjectHttpPutModel;

namespace ProjectManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationManager.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddDbContext<PmDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Common.AppConstants.ConnectionStringName))
            );

            services.AddControllersWithViews();

            services.AddTransient<IDeveloperService, DeveloperService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ILinkDevToProjectService, LinkDevToProjectService>();

            #region Repository
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            #region FluentValidators
            services.AddTransient<IValidator<CreateDeveloperHttpPostModel>, CreateDeveloperHttpPostModelValidator>();
            services.AddTransient<IValidator<CreateProjectHttpPostModel>, CreateProjectHttpPostModelValidator>();
            services.AddTransient<IValidator<UpdateProjectHttpPutModel>, UpdateProjectHttpPutModelValidator>();
            services.AddTransient<IValidator<UpdateDeveloperHttpPutModel>, UpdateDeveloperHttpPutModelValidator>();
            services.AddTransient<IValidator<LinkDevToProjectHttpPostModel>, LinkDevToProjectHttpPostModelValidator>();
            #endregion
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

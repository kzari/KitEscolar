using Kzari.MateriaisEscolares.Application.AppServices;
using Kzari.MateriaisEscolares.Application.AppServices.Interfaces;
using Kzari.MateriaisEscolares.Infra.Data;
using Kzari.MateriaisEscolares.Infra.Data.DbContexts;
using Kzari.MateriaisEscolares.Infra.Data.Repositories;
using Kzari.MateriaisEscolares.Web.Filters;
using Kzari.MateriaisEscolares.Web.Middlewares;
using Kzari.MaterialEscolar.Domain.Interfaces.DbContexts;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kzari.MateriaisEscolares.Web
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
            services.AddMvc(opt => opt.Filters.Add(typeof(ValidatorActionFilter)));
            
            MapAppServices(services);

            MapRepositories(services);

            services.AddDbContext<MEContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString(nameof(MEContext))));

            // Map & Get new service to IApplicationDbContext with MEContext
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<MEContext>());
        }

        private static void MapAppServices(IServiceCollection services)
        {
            services.AddTransient<IKitAppService, KitAppService>();
        }
        private static void MapRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

            services.AddTransient<IKitRepository, KitRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandlerValidator();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

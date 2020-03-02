using Kzari.KitEscolar.Application.AppServices;
using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Infra.Data;
using Kzari.KitEscolar.Infra.Data.DbContexts;
using Kzari.KitEscolar.Web.Filters;
using Kzari.KitEscolar.Web.Middlewares;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Kzari.KitEscolar.Application.MappingProfiles;
using Kzari.KitEscolar.Infra.Data.Repositories;

namespace Kzari.KitEscolar.Web
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
            services
                .AddMvc(opt => opt.Filters.Add(typeof(ValidatorActionFilter)))
                .AddNewtonsoftJson();


            MapAppServices(services);

            MapRepositories(services);

            services.AddDbContext<MEContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString(nameof(MEContext))));

            // Map & Get new service to IApplicationDbContext with MEContext
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<MEContext>());


            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new EntityToModelMappingProfile());
                opt.AddProfile(new ModelToEntityMappingProfile());
            }, typeof(Startup));
        }

        private static void MapAppServices(IServiceCollection services)
        {
            services.AddTransient<IKitAppService, KitAppService>();
            services.AddTransient<IProdutoAppService, ProdutoAppService>();
        }
        private static void MapRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

            services.AddScoped<IKitRepository, KitRepository>();
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

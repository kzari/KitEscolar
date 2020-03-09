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
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;
using FluentValidation;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Validators;
using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Domain;

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
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
            });

            services
                .AddMvc(opt => {
                    opt.Filters.Add(typeof(ValidatorActionFilter));
                    opt.EnableEndpointRouting = false; })
                .AddNewtonsoftJson();

            MapAppServices(services);
            MapRepositories(services);
            MapValidators(services);

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

        private static void MapValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<Kit>, KitValidator>();
            services.AddTransient<IValidator<Produto>, ProdutoValidator>();
        }

        private static void MapAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
            services.AddTransient(typeof(IAppServiceBase<,>), typeof(AppServiceBase<,>));
            services.AddTransient(typeof(IAppServiceBase<,,>), typeof(AppServiceBase<,,>));

            services.AddTransient<IKitAppService, KitAppService>();
        }
        private static void MapRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

            //services.AddScoped<IKitRepository, KitRepository>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var ci = new CultureInfo("pt-BR");
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = new[] { ci },
                SupportedUICultures = new[] { ci }
            });

            app.UseExceptionHandlerValidator();


            if (env.IsDevelopment())
            {
                app.UseExceptionHandlerValidator();
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandlerValidator();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

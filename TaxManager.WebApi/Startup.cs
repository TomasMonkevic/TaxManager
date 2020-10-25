using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxManager.Contracts;
using TaxManager.Persistence;
using TaxManager.Persistence.Repository;
using TaxManager.Service;
using TaxManager.WebApi.Filters;
using TaxManager.WebApi.Mappers;

namespace TaxManager.WebApi
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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionHandlerFilter));
            });
            services.AddSwaggerGen();
            services.AddDbContext<TaxManagerContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("TaxManager"))
            );
            services.AddMvc().AddFluentValidation();

            services.AddScoped<TaxManagerContext>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<IMunicipalityService, MunicipalityService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<ITaxScheduleMapper, TaxScheduleMapper>();

            services.AddTransient<IValidator<TaxRateRequest>, TaxRateRequestValidator>();
            services.AddTransient<IValidator<TaxScheduleRequest>, TaxScheduleRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxMananger API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

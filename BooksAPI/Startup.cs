using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.BooksMapper;
using BooksApi.Data;
using BooksApi.Repository;
using BooksApi.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BooksAPI
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHFqVVhkW1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jT35QdkBhXn5Xc3ZXQQ==;Mgo+DSMBPh8sVXJ0S0d+XE9AcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTcEVgWXpcd3VcRWVcVg==;ORg4AjUWIQA/Gnt2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkNjWn9bcnNUT2NaVEM=;NjUwMzg0QDMyMzAyZTMxMmUzMFBQS2tmUE93QWtPM0FxQmgybThUc3JMSHNYeml4NXdTVXZDNGR3enp0UlE9;NjUwMzg1QDMyMzAyZTMxMmUzMENPcWkyTnNIWmdPM2gyOE9VaFc1TWwvem1TSVFIWDEzZG9nUWtVbTBKVm89;NRAiBiAaIQQuGjN/V0Z+XU9EaFtFVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVlWXxedXZSR2lYVUdx;NjUwMzg3QDMyMzAyZTMxMmUzMEIyTkFEclh5V3I4OC9IclB4V3NNaDlaM2V0OVBSSXF2NUtqWHZlWnhZYzQ9;NjUwMzg4QDMyMzAyZTMxMmUzMG0xOWJMWWwvMWYvOUJRQ3cyU3E3ZW9DZWVDVkliL2JqUDdFdGtqdkNGTW89;Mgo+DSMBMAY9C3t2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkNjWn9bcnNUT2VbUEw=;NjUwMzkwQDMyMzAyZTMxMmUzMGoyblI1U1RHUWJ1RnE4Y21TZ0dDd2JKRGRETXNvdi9YUDh0NDdzWHhjTVE9;NjUwMzkxQDMyMzAyZTMxMmUzMExkbU5aZzk0OXdoc29uTDJLWFdITmE0d2huUEZuOHdzWlBSSElQQU9FQkU9");
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("Default")));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(BooksMappings));
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("BooksOpenAPISpec",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "Books Api",
                            Version = "0.1"
                        });
                }
            );
            services.AddControllers();
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
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/BooksOpenAPISpec/swagger.json", "Books API ");
                    options.RoutePrefix = "";
                }
            );
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

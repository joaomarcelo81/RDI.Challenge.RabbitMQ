using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RDI.Challenge.Business;
using RDI.Challenge.DataContext;
using RDI.Challenge.DataContext.Repository;
using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Business;
using RDI.Challenge.Domain.Interfaces.Repositories;
using RDI.Challenge.Domain.Interfaces.Repositories.Rabbit;
using RDI.Challenge.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace RDI.Challenge.UI
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
            services.AddDbContext<ChallengeContext>(opt => opt.UseInMemoryDatabase("ChallengeContext"));
            services.AddControllers();
            DefineInjection(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RDI Challenge",
                    Description = "A simple RDI Challenge API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Joao marcelo Guarana",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/joaomarcelomello/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var context = app.ApplicationServices.GetService<ChallengeContext>();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChallengeContext>();

                AddLocalData(context);
                // Seed the database.
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }

        private static void DefineInjection(IServiceCollection services)
        {
            
            
           
            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<ChallengeContext>();
            services.AddTransient(typeof(IBaseBusiness<>), typeof(BaseBusiness<>));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient(typeof(IOrderBusiness), typeof(OrderBusiness));
            services.AddTransient(typeof(IRabbitRepository), typeof(RabbitRepository));
            
                services.AddTransient(typeof(IMenuItemRepository), typeof(MenuItemRepository));
            services.AddTransient(typeof(IMenuItemBusiness), typeof(MenuItemBusiness));
            
        }

        private static void AddLocalData(ChallengeContext context)
        {
            context.Add<MenuItem>(
            new MenuItem
            {
                MenuItemId = 1,
                Name = "French Fries",
                Area = "fries",
                Description = "Potato Fries"

            });
            context.Add<MenuItem>(
            new MenuItem
            {
                MenuItemId = 2,
                Name = "T-Bone",
                Area = "grill",
                Description = "T-Bone Steak"

            });
            context.Add<MenuItem>(
            new MenuItem
            {
                MenuItemId = 3,
                Name = "Ceaser",
                Area = "salad",
                Description = "Salad with Ceaser Sauce"

            });
            context.Add<MenuItem>(
            new MenuItem
            {
                MenuItemId = 4,
                Name = "Coca Cola",
                Area = "drink",
                Description = "Soda"

            });
            context.Add<MenuItem>(
            new MenuItem
            {
                MenuItemId = 5,
                Name = "Ice Cream",
                Area = "desert",
                Description = "Ice Cream"

            });
            context.SaveChanges();
        }
    }
}

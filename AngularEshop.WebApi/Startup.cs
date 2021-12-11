using AngularEshop.Common;
using AngularEshop.Data.Context;
using AngularEshop.Data.Contracts;
using AngularEshop.Data.Repositories;
using AngularEshop.Services.Helpers;
using AngularEshop.Services.Implamentations;
using AngularEshop.Services.Intefaces;
using AngularEshop.WebFramework.Configuration;
using AngularEshop.WebFramework.Middlewares;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting;

namespace AngularEshop.WebApi
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(Configuration["ConnectionStrings:Development"]));
            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();


            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
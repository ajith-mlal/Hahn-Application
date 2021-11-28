using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Hahn.ApplicationProcess.July2021.Data.Configuration;
using Hahn.ApplicationProcess.July2021.Data.Data;
using Hahn.ApplicationProcess.July2021.Data.Entities;
using Hahn.ApplicationProcess.July2021.Domain.Interfaces;
using Hahn.ApplicationProcess.July2021.Domain.Validators;
using Hahn.ApplicationProcess.July2021.Domain.Dto ;
using FluentValidation.AspNetCore;
using FluentValidation;
using AutoMapper;

namespace Hahn.ApplicationProcess.July2021.Web
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
            services.AddDbContext<DBContext>(options =>
            {
                options.UseInMemoryDatabase("TestApp");
            });

            services.AddControllers().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
              
            });
            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UOW_101", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddCors();
            services.AddScoped<IUnitofWork, UnitOfWork>();
            services.AddScoped<IApiClient, ApiClient>();
            services.AddScoped<IValidator<UserProfileDto>, UserProfileValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UOW_101 v1"));
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:8080")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

        }

        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<UserProfileDto, UserProfile>();
                CreateMap<UserAssetDto, UserAssets>();
                CreateMap<UserAddressDto, UserAddress>();
            }
        }
    }
}

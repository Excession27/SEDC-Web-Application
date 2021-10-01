using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SEDCWebAPI.Middlewares;
using SEDCWebAPI.Services.Implementation;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic;
using SEDCWebApplication.BLL.Logic.Implementations;
using SEDCWebApplication.BLL.Logic.Interfaces;


//  DAL ADO.net
/*using SEDCWebApplication.DAL.Data.Implementations;
using SEDCWebApplication.DAL.Data.Interfaces;*/

// DAL EF EntityFactory
//using SEDCWebApplication.DAL.EntityFactory.Implementation;
//using SEDCWebApplication.DAL.EntityFactory.Interfaces;

// DAL EF DatabaseFactory
using SEDCWebApplication.DAL.DatabaseFactory;
using SEDCWebApplication.DAL.DatabaseFactory.Implementations;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;


using SEDCWebApplication.Models.Repositories.Implementations;
using SEDCWebApplication.Models.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebAPI
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
            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(typeof(EmployeeManager));
            services.AddAutoMapper(typeof(CustomerManager));
            services.AddAutoMapper(typeof(ProductManager));
            services.AddAutoMapper(typeof(UserManager));
            services.AddAutoMapper(typeof(OrderManager));


            services.AddScoped<IEmployeeRepository, DatabaseEmployeeRepository>();
            services.AddScoped<ICustomerRepository, DatabaseCustomerRepository>();
            services.AddScoped<IProductRepository, DatabaseProductRepository>();
            services.AddScoped<IOrderRepository, DatabaseOrderRepository>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<>


            //BLL
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IOrderManager, OrderManager>();

            //DAL
            /*            services.AddScoped<IEmployeeDAL, EmployeeDAL>();*/
            /*            services.AddScoped<ICustomerDAL, CustomerDAL>();
                        services.AddScoped<IProductDAL, ProductDAL>();*/

            //EntityFramework - Entity First
            /*            services.AddScoped<IEmployeeDAL, EmployeeRepository>();
                        services.AddScoped<ICustomerDAL, CustomerRepository>();
                        services.AddScoped<IProductDAL, ProductRepository>();
                        services.AddScoped<IOrderDAL, OrderRepository>();*/

            //EntityFramework - Database First
            services.AddScoped<IEmployeeDAL, EmployeeRepositoryDF>();
            services.AddScoped<ICustomerDAL, CustomerRepositoryDF>();
            services.AddScoped<IProductDAL, ProductRepositoryDF>();
            services.AddScoped<IOrderDAL, OrderRepositoryDF>();
            services.AddScoped<IUserDAL, UserRepositoryDF>();

            /*            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                        options =>
                        {
                            options.LoginPath = new PathString("/");
                            options.AccessDeniedPath = new PathString("/auth/denied");
                            });*/


            // configuration for JWT Authentication

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings")["Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SEDC_WebAPI", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SEDCEF")));



            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(provider.GetService<IConfiguration>()));
            }).CreateMapper());

            services.AddCors(option => option.AddPolicy("PolicyOne", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            

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

            app.UseCors("PolicyOne");

            app.UseAuthentication();
            app.UseAuthorization();
            

            

            //custom jwt middleware
            app.UseMiddleware<JwtMiddleware>();

            //global error handling
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("v1/swagger.json", "SEDC_WebAPI v1");
                 
             });

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

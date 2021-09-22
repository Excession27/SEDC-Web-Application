using Microsoft.AspNetCore.Authentication.Cookies;
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
using SEDCWebAPI.Services.Implementation;
using SEDCWebAPI.Services.Interfaces;
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


            services.AddScoped<IEmployeeRepository, DatabaseEmployeeRepository>();
            services.AddScoped<ICustomerRepository, DatabaseCustomerRepository>();
            services.AddScoped<IProductRepository, DatabaseProductRepository>();
            services.AddScoped<IUserService, UserService>();

            //BLL
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IUserManager, UserManager>();

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SEDC_WebAPI", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SEDCEF")));

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

            app.UseAuthorization();

            app.UseCors("PolicyOne");

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

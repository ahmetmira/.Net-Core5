using GRTekrar.Api.Helpers;
using GRTekrar.Buisness.Abstract;
using GRTekrar.Buisness.Concrete;
using GRTekrar.DataAccess;
using GRTekrar.DataAccess.Abstract;
using GRTekrar.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace GRTekrar.Api
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
            //services.AddDbContext<GRTekrarDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("Default")));
            //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<GRTekrarDbContext>();
            services.AddDbContext<GRTekrarDbContext>(_dbContext => _dbContext.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages();
            services.AddSwaggerDocument();


            // MediatR
            //var assembly = AppDomain.CurrentDomain.Load("Application");
            //services.AddMediatR(assembly);
            services.AddMediatR(typeof(Startup));
            services.AddControllersWithViews();
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //.AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            //FluentValidation
            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            //for Application
            //services.AddControllersWithViews();
            // services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);


            //Repository gerek yok
            //services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<GenericHelperMethods>();
            //services.AddHttpContextAccessor();  dışardan Api


            //services.AddMvc().AddRazorPagesOptions(opt => opt.Conventions.AddPageRoute("/Login", ""));// Login Anasayfa olması için yaptık
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience=true,          //Token değerini kimlerin-hangi uygulamaların kullanacağını belirler
                ValidateIssuer=true,            //Oluşturulan token değerini kim dağıtmıştır
                ValidateLifetime=true,          //Oluşturulan token değerinin yaşam süresi
                ValidateIssuerSigningKey=true,  //Üretilen token değerinin uygulamamıza ait olup olmadığı ile alakalı 
                ValidIssuer=Configuration["token:Issuer"],
                ValidAudience=Configuration["token:Audience"],
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                ClockSkew=TimeSpan.Zero         //Token süresinin uzatılmasını sağlar
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            //Kökler Arası Kaynak Paylaşımı 
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}



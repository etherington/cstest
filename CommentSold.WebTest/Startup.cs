﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using CommentSold.WebTest.Repositories;
using CommentSold.WebTest.Repositories.Caching;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CommentSold.WebTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationIdentityUser, ApplicationRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
            });
         
            //Redis cache setup
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));
            services.AddSingleton<ICacheStorage, AzureCacheStorage>(options => new AzureCacheStorage(Configuration.GetConnectionString("Redis"), Constants.DefaultCacheSeconds));

            services.AddMvc(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            //Flash message injection
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Vereyon.Web.IFlashMessage, Vereyon.Web.FlashMessage>();

            //Repositories and cache injection
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<ICachingProductRepository, CachingProductStore>();
            services.AddScoped<ICachingInventoryRepository, CachingInventoryStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Inventory, InventoryDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                        src.Product.ProductName))
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src =>
                        src.Product.Id));
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, ProductForListDto>(); 

                cfg.CreateMap<ProductDto, ProductForEditDto>(MemberList.Destination) 
                    .ForSourceMember(x => x.Inventories, opt => opt.DoNotValidate());
             
                cfg.CreateMap<Inventory, SkuDto>();

                cfg.CreateMap<PagedList<Product>, PagedList<ProductForListDto>>()
                    .ConvertUsing<PagedListAutomapperConverter<Product, ProductForListDto>>();
                cfg.CreateMap<PagedList<Product>, PagedList<ProductDto>>()
                    .ConvertUsing<PagedListAutomapperConverter<Product, ProductDto>>();
                cfg.CreateMap<PagedList<ProductDto>, PagedList<ProductForListDto>>()
                    .ConvertUsing<PagedListAutomapperConverter<ProductDto, ProductForListDto>>();
                cfg.CreateMap<PagedList<Inventory>, PagedList<InventoryDto>>()
                    .ConvertUsing<PagedListAutomapperConverter<Inventory, InventoryDto>>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

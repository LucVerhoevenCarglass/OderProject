using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using Order.Api.Controllers.Customers;
using Order.Api.Controllers.Items;
using Order.Api.Controllers.Orders;
using Order.Api.Controllers.Users;
using Order.Api.Helper;
using Order.Services.Customers;
using Order.Services.Items;
using Order.Services.Orders;
using Order.Services.Users;

namespace Order.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddSingleton<IOrderMapper, OrderMapper>();
            services.AddSingleton<ICustomerMapper, CustomerMapper>();
            services.AddSingleton<IItemMapper, ItemMapper>();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Customer", policy => policy.RequireRole("Customer", "Admin"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMvc();

            //app.Run(async context =>
            //{
            //    context.Response.Redirect("/swagger");
            //});
        }
    }
}

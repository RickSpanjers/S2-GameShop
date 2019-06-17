using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;

namespace RicksWebWorld
{
	public class Startup
	{
		public static string Connectionstring;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			AutoMapperExtension.Initialize();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddMvc();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddDistributedMemoryCache();
			services.AddSingleton(Configuration);
			services.AddSingleton<IConfiguration>(Configuration);
			services.AddSession(options =>
			{
				// Set a short timeout for easy testing.
				options.Cookie.Name = ".Session.Login";
				options.IdleTimeout = TimeSpan.FromMinutes(15);
				options.Cookie.HttpOnly = true;
			});


			Connectionstring = Configuration.GetConnectionString("AzureConnection");
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			// IMPORTANT: This session call MUST go before UseMvc()
			app.UseSession();
			app.UseDeveloperExceptionPage();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}");

				routes.MapRoute(
					name: "login",
					template: "{controller=User}/{action=Login}");

				routes.MapRoute(
					name: "dashboard",
					template: "{controller=Home}/{action=Dashboard}");

				routes.MapRoute(
					name: "categoryoverview",
					template: "{controller=Category}/{action=Categoryoverview}");

				routes.MapRoute(
					name: "productoverview",
					template: "{controller=Product}/{action=Productoverview}");

				routes.MapRoute(
					name: "singleproduct",
					template: "{controller=Product}/{action=SingleProduct}");

				routes.MapRoute(
					name: "singleuser",
					template: "{controller=User}/{action=SingleUser}");

				routes.MapRoute(
					name: "singleuseredit",
					template: "{controller=User}/{action=SingleUserEdit}");

				routes.MapRoute(
					name: "useroverview",
					template: "{controller=User}/{action=Useroverview}");

				routes.MapRoute(
					name: "permissionoverview",
					template: "{controller=Permission}/{action=Permissionoverview}");

				routes.MapRoute(
					name: "createnewuser",
					template: "{controller=User}/{action=CreateNewUserPage}");

				routes.MapRoute(
					name: "cart",
					template: "{controller=Cart}/{action=Cart}");

				routes.MapRoute(
					name: "checkout",
					template: "{controller=Cart}/{action=Checkout}");
			});
		}
	}
}
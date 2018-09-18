using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ExampleHero.Api.Middleware.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using ExampleHero.Api.Middleware.RouteConstraints;

namespace ExampleHero.Api
{
	public class Startup
	{
		public Startup(IHostingEnvironment environment)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(environment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();

			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
			};

		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.RegisterServices(Configuration);

			services.AddMvc(options =>
				{
					options.RespectBrowserAcceptHeader = true;
					options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
				}).AddXmlSerializerFormatters()
				.AddXmlDataContractSerializerFormatters()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.Configure<RouteOptions>(routeOptions =>
			{
				routeOptions.ConstraintMap.Add("iso8601date", typeof(Iso8601DateTimeOffsetConstraint));
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "ExapmleHero API",
					Description = "A simple example ASP.NET Core Web API",
					TermsOfService = "None",
					Contact = new Contact
					{
						Name = "Mykhailo Kubarych",
						Email = "mishanyakub21@gmail.com"
					}
				});
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseStaticFiles();

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.ExceptionMiddleware();
			app.UseMvcWithDefaultRoute();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExampleHero API");
				c.RoutePrefix = "swagger";
			});

			app.DbSeed(Configuration);
		}
	}
}

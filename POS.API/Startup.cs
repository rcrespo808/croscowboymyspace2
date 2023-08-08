using POS.API.Helpers;
using POS.API.Helpers.Mapping;
using POS.Data;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.PipeLineBehavior;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using POS.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace POS.API
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
            var connectionstring = Configuration.GetConnectionString("DbConnectionString");
            var assembly = AppDomain.CurrentDomain.Load("POS.MediatR");
            var defaultUserId = Configuration.GetSection("DefaultUser").GetSection("DefaultUserId").Value;
            var pathHelper = new PathHelper(Configuration);
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(Enumerable.Repeat(assembly, 1));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            JwtSettings settings;
            settings = GetJwtSettings();
            services.AddSingleton(settings);
            services.AddTransient<JobService>();
            services.AddSingleton(pathHelper);
            services.AddSingleton<IConnectionMappingRepository, ConnectionMappingRepository>();
            services.AddScoped(c => new UserInfoToken() { Id = defaultUserId });

            services.AddDbContextPool<POSDbContext>(options =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
                options.UseMySql(Configuration.GetConnectionString("DbConnectionString"), serverVersion).EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.ConfigureWarnings(builder =>
                {
                    builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
                });
            });
            services.AddIdentity<User, Role>()
             .AddEntityFrameworkStores<POSDbContext>()
             .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            services.AddSingleton(MapperConfig.GetMapperConfigs(pathHelper));
            services.AddDependencyInjection();
            services.AddJwtAutheticationConfiguration(settings);
 /*           
            services.AddCors(options =>
            {
                options.AddPolicy("ExposeResponseHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4000",
                                            "http://localhost:4200",
                                            "https://localhost:4000",
                                            "https://localhost:4200",
                                            "http://localhost:4201",
                                            "https://caincos.megapp10.com",
                                            "https://cyberweek.shigongbolivia.com",
                                            "http://143.208.182.105:4200",
                                            "http://14382.208.1.105", 
                                            "http://143.208.182.108:4200",
                                            "http://143.208.182.108", 
                                            "http://54.82.76.186/:4200",
                                            "http://54.82.76.186/",
                                            "https://app-cainco.cainco.org.bo:4200",
                                            "https://app-cainco.cainco.org.bo"

                                         )
                               .WithExposedHeaders("X-Pagination")
                               .AllowAnyHeader()
                               .AllowCredentials()
                               .WithMethods("POST", "PUT", "PATCH", "GET", "DELETE")
                               .SetIsOriginAllowed(host => true);
                    });
            });
*/
            services.AddSignalR();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "POS API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //var jobService = sp.GetService<JobService>();
            //jobService.StartScheduler();
            SpaStartup.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            }
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint($"v1/swagger.json", "Chem Website");
                c.RoutePrefix = "swagger";
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx => {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
                      "Origin, X-Requested-With, Content-Type, Accept");
                },
            });

            //app.UseCors("ExposeResponseHeaders");
            // Habilitar CORS para permitir todas las solicitudes de origen (Desactivar CORS)
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<UserHub>("/userHub");
            });
            SpaStartup.Configure(app);
        }

        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = Configuration["JwtSettings:key"];
            settings.Audience = Configuration["JwtSettings:audience"];
            settings.Issuer = Configuration["JwtSettings:issuer"];
            settings.MinutesToExpiration =
             Convert.ToInt32(
                Configuration["JwtSettings:minutesToExpiration"]);

            return settings;
        }
    }
}

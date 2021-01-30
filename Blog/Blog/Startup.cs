using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common;
using Blog.Core.Domain;
using Blog.Core.Domain.PostBlog;
using Blog.Core.Domain.Registration;
using Blog.Core.Domain.User;
using Blog.Core.Mapping;
using Blog.Data.Reader.PostBlog;
using Blog.Data.Reader.User;
using Blog.Data.Writer;
using Blog.Data.Writer.PostBlog;
using Blog.Data.Writer.Registration;
using Blog.Model.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Blog
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
            //services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddMapping();
            var databaseSettingsConfig = new DatabaseSettingsConfig
            {
                ConnectionString = Configuration["ConnectionString"]
            };

            services.AddMvc();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                     .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });

            services.AddSingleton(databaseSettingsConfig);


            // configure strongly typed settings objects
            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            //to validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });




            services.AddSingleton<IObjectMapper, ObjectMapper>();
            services.AddScoped<IDatabaseSettings, DatabaseSettings>();

            services.AddScoped<IRegistrationDataWriter, RegistrationDataWriter>();
            services.AddScoped<IRegistrationWriter, RegistrationWriter>();

            services.AddScoped<IUserDataReader, UserDataReader>();
            services.AddScoped<IUserReader, UserReader>();

            services.AddScoped<IPostBlogDataWriter, PostBlogDataWriter>();
            services.AddScoped<IPostBlogWriter, PostBlogWriter>();

            services.AddScoped<IPostBlogDataReader, PostBlogDataReader>();
            services.AddScoped<IPostBlogReader, PostBlogReader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseMvc();
        }
    }
}

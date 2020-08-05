using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
using test_dotnet_webapi.Data;
using test_dotnet_webapi.Services.CharacterService;

namespace test_dotnet_webapi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // add db link
            services.AddDbContext<DataContext> (x => x.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));

            // add controllers (mvc)
            services.AddControllers ();

            // add mapper for dto mapping
            services.AddAutoMapper (typeof (Startup)); // added after install of AutoMapper (cmd: dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection)

            // register services
            services.AddScoped<ICharacterService, CharacterService> ();
            services.AddScoped<IAuthRepository, AuthRepository> ();

            // add auth with options
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (Configuration.GetSection ("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // provide user context in services/repo
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            // app.UseHttpsRedirection();
            app.UseRouting ();
            app.UseAuthentication();
            // add token authorization (used in controllers to access certain methods)
            app.UseAuthorization ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}
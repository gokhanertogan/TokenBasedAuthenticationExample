using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TokenBasedAuthentication.API.Domain;
using TokenBasedAuthentication.API.Domain.Repositories;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Domain.UnitOfWork;
using TokenBasedAuthentication.API.Security.Token;
using TokenBasedAuthentication.API.Services;

namespace TokenBasedAuthentication.API
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
            services.AddScoped<IAuthenticationService,AuthenticationService>();
            services.AddScoped<ITokenHandler,TokenHandler>();    
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey=SingHandler.GetSecurityKey(tokenOptions.SecurityKey)
                };
            });

            services.AddDbContext<TokenContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("TokenDbConnectionString")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TokenBasedAuthentication.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TokenBasedAuthentication.API v1"));
            }

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

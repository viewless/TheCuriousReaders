using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using TheCuriousReaders.API.Extensions;
using TheCuriousReaders.API.Middleware;
using TheCuriousReaders.DataAccess;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.DataAccess.Repositories;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authConfiguration = services.GetAuthConfig(this.Configuration);
            var userSubConfiguration = services.GetUserSubscriptionConfig(this.Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheCuriousReaders.API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);

            }).AddSwaggerGenNewtonsoftSupport();

            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            services
            .AddDbContext<CuriousReadersContext>(options => options
            .UseSqlServer(Configuration.GetDefaultConnectionString()))
            .AddIdentity()
            .AddJwtAuthentication(authConfiguration)
            .AddControllers()
            .AddNewtonsoftJson();

            services.AddSingleton(authConfiguration);
            services.AddSingleton(userSubConfiguration);
            services.AddSingleton(options => {
                return new BlobServiceClient(Configuration.GetAzureConnectionString());
            });

            services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            services.AddScoped<ISubscriptionsService, SubscriptionsService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBlobService, BlobService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheCuriousReaders.API v1"));

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

using BookXChangeApi.Util.Swagger;
using EmployeeManagementSystemAPI.Constants;
using EmployeeManagementSystemAPI.Util.Swagger;
using EmployeeManagementSystemDB.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystemAPI
{
    public static class ProgramServices
    {
        public static void AddServices(WebApplicationBuilder builder, string connectionString, bool sqlLoggingEnabled)
        {
            ConfigureCoreServices(builder);
            AddSwagger(builder);
            AddDatabaseContextFactory(builder, connectionString, sqlLoggingEnabled);

            AddBusinessLayer(builder);
            AddFirebaseJWTAuthentication(builder);
            builder.Services.AddHttpClient();
            builder.Services.AddCors();
            builder.Services.AddHealthChecks(); // Health checks required for AWS.
        }

        private static void ConfigureCoreServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        private static void AddBusinessLayer(WebApplicationBuilder builder)
        {
            /// Inject a ready-only database context, which is used by <see cref="Queries{T}"/> classes
            //builder.Services.AddScoped<IReadOnlyDBContext<WowDbContext>, ReadOnlyDBContext<WowDbContext>>();
            //builder.Services.AddScoped<IFirebaseHandler, FirebaseHandler>();

            /// Inject Business and Query Layers:
        }

        private static void AddFirebaseJWTAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://securetoken.google.com/{MainConstants.FirebaseID}";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://securetoken.google.com/{MainConstants.FirebaseID}",
                    ValidateAudience = true,
                    ValidAudience = MainConstants.FirebaseID,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(60)
                };
            });
        }

        private static void AddDatabaseContextFactory(WebApplicationBuilder builder, string connectionString, bool sqlLoggingEnabled)
        {
            var serverVersion = ServerVersion.AutoDetect(connectionString); // This can throw an exception if we cannot connect to the database.

            builder.Services.AddDbContextFactory<EmpDatabaseContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);

                if (!sqlLoggingEnabled)
                {
                    dbContextOptions.UseLoggerFactory(LoggerFactory.Create(builder => builder.ClearProviders()));
                }
            });
        }

        private static void AddSwagger(WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.DocumentFilter<SwaggerTagFilter<EMSTags>>();

                c.AddBearerSecurityDefinition();
                c.AddBearerSecurityRequirement();
            });
        }
    }
}
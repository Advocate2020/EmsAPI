using EmployeeManagementSystemAPI;
using EmployeeManagementSystemAPI.Constants;
using EmployeeManagementSystemAPI.Google;
using EmployeeManagementSystemAPI.Util.Swagger;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var _sm = new GoogleSecretManagerService();

var builder = WebApplication.CreateBuilder(args);

var connectionString = MainConstants.ConnectionString;

ProgramServices.AddServices(builder: builder, connectionString: connectionString, sqlLoggingEnabled: true);

var app = builder.Build();

// Setup health checks.
app.MapHealthChecks("/health");
app.MapHealthChecks("/");

app.UseCors(policy => policy
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

#region Swagger & Redoc

app.UseSwagger(options =>
{
    options.RouteTemplate = "/swagger/{documentName}/swagger.json";
});

app.MapScalarApiReference(options =>
{
    options.WithDarkMode(false);
    options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
    options.WithTitle("Wealth on Wheels API - Scalar");
    options.WithEndpointPrefix("/scalar/{documentName}"); // URL: /scalar/v1
});

app.UseSwaggerUI(options => { options.DocExpansion(DocExpansion.None); });
app.UseReDocUI("EM System");

#endregion Swagger & Redoc

#region Firebase Secrets

try
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile("C:\\Users\\SAKHANYA\\Documents\\Upskilling\\EmsAPI\\EmployeeManagementSystemAPI\\e-m-s-1dd64-firebase-adminsdk-fbsvc-312da5c2db.json")
    });
}
catch (Exception e)
{
    Console.WriteLine($"# Firebase setup failed : {e.Message}");
    throw;
}

#endregion Firebase Secrets

// Https redirect is handled on AWS, by an Application Load Balancer.
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
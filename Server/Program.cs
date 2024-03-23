using Constants;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NLog;
using NLog.Web;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.Features.Identity;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// **************************************************
NLog.LogManager.Setup().LoadConfigurationFromFile(configFile: "NLog.config");
builder.Host.UseNLog();
// **************************************************

var applicationSettings =
    builder.Configuration.GetSection
    (Infrastructure.Settings.ApplicationSettings.KeyName)
    .Get<Infrastructure.Settings.ApplicationSettings>();
// **************************************************

// **************************************************
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder =>
     builder.WithOrigins(CommonRouting.BaseClientUrl, "http://localhost:5292")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});
// **************************************************

// **************************************************
// **************************************************
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.Audience = applicationSettings!.tokenProfile.Audience;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(applicationSettings!.tokenProfile.SecretForKey)),
    };
});
// **************************************************
// **************************************************

// **************************************************
// **************************************************

builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
    (builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
    .AddSingleton
    (implementationFactory: serviceType =>
    {
        var result =
            serviceType.GetRequiredService
            <Microsoft.Extensions.Options.IOptions
            <Infrastructure.Settings.ApplicationSettings>>().Value;

        return result;
    });

// **************************************************
// **************************************************

// **************************************************
// **************************************************

builder.Services
            .AddDbContext<Persistence.DatabaseContext>(optionsAction: options =>
            {
                // using Microsoft.EntityFrameworkCore;
                options.UseLazyLoadingProxies();

                // using Microsoft.EntityFrameworkCore;
                //options.UseSqlServer(connectionString:
                //	applicationSettings.DatabaseSettings.SqlServerConnectionString);

                // using Microsoft.EntityFrameworkCore;
                options.UseSqlServer(connectionString: applicationSettings!
                    .ConnectionString, sqlServerOptionsAction: current =>
                    {
                        current.MigrationsAssembly
                            (assemblyName: "Persistence.SqlServer");
                    });
            });

// **************************************************
// **************************************************
#region container

// **************************************************
builder.Services
    .AddHttpContextAccessor();
//  *************************************************
builder.Services.AddControllers().
        ConfigureApiBehaviorOptions(setupAction: setupAction =>
    setupAction.InvalidModelStateResponseFactory = context =>
    {
        // create a validation problem details object
        var problemDetailsFactory = context.HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState);

        // add additional info not added by default
        //validationProblemDetails.Detail =
        //    "See the errors field for details.";
        validationProblemDetails.Instance =
            context.HttpContext.Request.Path;

        // report invalid model state responses as validation issues
     
        validationProblemDetails.Status =
            StatusCodes.Status422UnprocessableEntity;
        validationProblemDetails.Title =
            "ModelStateIsInVaild";

        return new UnprocessableEntityObjectResult(
            validationProblemDetails)
        {
            ContentTypes = { ContentTypes.UnprocessableEntity }
        };
    });
//  *************************************************
builder.Services.AddSwaggerGen();
//  *************************************************
builder.Services
    .AddScoped<Services.Features.Common.HttpContextService>();
// **************************************************
builder.Services
    .AddScoped<Services.Features.Identity.UserNotificationService>();
// **************************************************
builder.Services
    .AddScoped<JwtTokenService>();

builder.Services
    .AddScoped<Infrastructure.Security.UserManagerService>();

builder.Services
    .AddScoped<Infrastructure.Security.AuthenticatedUserService>();
// **************************************************

#endregion /container

var app = builder.Build();
// **************************************************
//app.UseGlobalException();
// **************************************************

app.UseGlobalException();
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");

//}

// **************************************************
// **************************************************
app.UseSwagger();
app.UseSwaggerUI();
// **************************************************
// **************************************************
app.UseHsts();
// **************************************************
app.UseHttpsRedirection();
// **************************************************
app.UseCors("_myAllowSpecificOrigins");
// **************************************************
app.UseStaticFiles();
// **************************************************
app.UseRouting();
// **************************************************
app.UseAuthentication();
app.UseAuthorization();
// **************************************************
app.MapControllers();
// **************************************************

app.Run();

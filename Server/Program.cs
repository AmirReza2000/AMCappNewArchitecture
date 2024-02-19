using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

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
// **************************************************
builder.Services
    .AddAuthentication(defaultScheme:
        Infrastructure.Security.Constants.Scheme.Default)

    .AddCookie(authenticationScheme:
        Infrastructure.Security.Constants.Scheme.Default)
        ;
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
builder.Services.AddControllers();
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
    .AddScoped<Infrastructure.Security.UserManagerService>();

builder.Services
    .AddScoped<Infrastructure.Security.AuthenticatedUserService>();
// **************************************************

#endregion /container

var app = builder.Build();
// **************************************************
//app.UseGlobalException();
// **************************************************
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

}
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

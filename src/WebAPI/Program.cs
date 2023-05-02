using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using Infrastructure.Identity.Services;
using Infrastructure.Persistance;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebAPI.Filters;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "EmreKayacikAPI",
            License = new OpenApiLicense { Name = "emrekayacik.com" },
            Description = "Emre KAYACIK - API",
            Version = "V1.0.0",
            Contact = new OpenApiContact
            {
                Name = "Emre KAYACIK",
                Email = "emrekayacik2634@gmail.com"
            }

        });
    // Add security definitions
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    c.OperationFilter<AuthResponsesOperationFilter>();
});

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(x=>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Debug);
    x.AddDebug();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<JwtMiddleware>();

app.Run();

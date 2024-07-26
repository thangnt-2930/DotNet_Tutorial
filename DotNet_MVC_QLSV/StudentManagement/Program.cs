using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthen.Services.BasicAuthenticationHandler>("BasicAuthentication", null);

SeriLogInfo.Configure();

builder.Host.UseSerilog();

builder.Services.AddDbContext<StudentContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    // Cấu hình Basic Authentication
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                },
                Scheme = "basic",
                Name = "basic",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;  // Set Swagger UI at app's root
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using System.Text;
using API.Data;
using API.Extensions;
using API.MiddleWare;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
 
 // Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

 var app = builder.Build();
 app.UseMiddleware<ExceptionMiddleware>();
 // Configure the HTTP request pipeline.
 app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

 app.UseAuthentication();

 app.UseAuthorization();

 app.MapControllers();

 using var scope = app.Services.CreateScope();
 var services = scope.ServiceProvider;
 try
 {
     var context = services.GetRequiredService<DataContext>();
     await context.Database.MigrateAsync();
     await Seed.SeedUsers(context);
 }
 catch (Exception ex)
 {
     var logger = services.GetRequiredService<ILogger<Program>>();
     logger.LogError(ex, "An error occurred during migration");
 }
 
 
 app.Run();

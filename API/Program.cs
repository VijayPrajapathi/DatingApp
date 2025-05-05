using System.Text;
using API.Extensions;
using API.MiddleWare;


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
 
 app.Run();

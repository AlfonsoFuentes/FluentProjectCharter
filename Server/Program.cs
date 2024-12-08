using Server.MiddleWare;
using Server.RegisterServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServerServices();

var app = builder.Build();

app.UseExceptionHandling(app.Environment);

app.UseForwarding(builder.Configuration);

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
//app.UseCors("cors");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Initialize(builder.Configuration);


app.MapEndPoint();
app.UseEndpoints();

app.Run();

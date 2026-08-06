var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok("FlowCommerce is running."));

app.Run();

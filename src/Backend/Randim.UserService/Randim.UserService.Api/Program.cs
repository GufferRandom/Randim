using Randim.Common.DataAccess.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(config["ConnectionStrings:PostgreSql"]!);
var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapScalarApiReference();
app.Run();

using Randim.Common.DataAccess.Extensions;
using Randim.Common.Shared.Extensions;
using Randim.UserService.DataAccess.Interfaces;
using Randim.UserService.DataAccess.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddJwtTokenValidator(config);
builder.Services.AddDatabase(config["ConnectionStrings:PostgreSql"]!);
builder.Services.AddScoped<IUserFriendshipManager, UserFriendshipManager>();
var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapScalarApiReference();
app.Run();

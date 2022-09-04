using DataAccess;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using WebUI.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddDomain(builder.Configuration);
    builder.Services.AddDataAccess();
    builder.Services.AddCustomIdentity(builder.Configuration);
    builder.Services.AddAuthSwagger();
}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            DataAccess.Extensions.SeedData(userManager, roleManager);
        }
        catch
        {

        }
    }
    app.Run();
}
using DataAccess.Repositories;
using DataAccess.Repositories.Implementations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static void SeedData(UserManager<User> userManager,
                 RoleManager<IdentityRole<int>> roleManager)

    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }
    private static void SeedUsers(UserManager<User> userManager)
    {
        if (userManager.FindByNameAsync("admin@example.com").Result == null)
        {
            User user = new User();
            user.UserName = "admin@example.com";
            user.Email = "admin@example.com";
            user.FirstName = "Admin";
            user.LastName = "Admin";
            IdentityResult result = userManager.CreateAsync
            (user, "Admin123").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user,
                                    "Admin").Wait();
            }
        }
    }
    private static void SeedRoles(RoleManager<IdentityRole<int>> roleManager)
    {
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole<int> role = new IdentityRole<int>();
            role.Name = "User";
            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }


        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole<int> role = new IdentityRole<int>();
            role.Name = "Admin";
            IdentityResult roleResult = roleManager.
            CreateAsync(role).Result;
        }
    }

}

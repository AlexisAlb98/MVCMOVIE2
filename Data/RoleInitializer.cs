using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class RoleInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roleNames = { "Administrador", "Cliente" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Crear un usuario por defecto
        var user = new IdentityUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true
        };

        string userPassword = "Admin123_";
        var userExists = await userManager.FindByEmailAsync(user.Email);

        if (userExists == null)
        {
            var createPowerUser = await userManager.CreateAsync(user, userPassword);
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrador");
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task SeedIdentityUsers(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                // Voeg rollen toe (bijv. Admin, User)
                var roleExists = await roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Voeg een gebruiker toe (indien nodig)
                var user = await userManager.FindByEmailAsync("admin@example.com");
                if (user == null)
                {
                    user = new IdentityUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com"
                    };
                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (!result.Succeeded)
                    {
                        // Log de foutmelding
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }

                // Voeg de gebruiker toe aan een rol
                if (!await userManager.IsInRoleAsync(user, "Admin"))
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            catch (Exception ex)
            {
                // Log de exception
                Console.WriteLine($"Error seeding identity users: {ex.Message}");
            }
        }
    }
}

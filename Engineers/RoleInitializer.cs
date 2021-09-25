using Engineers.Models; 
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Engineers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string adminEmail = "admin@admin.com";
            const string adminName = "admin";
            const string password = "_gaij5ohMohba";

            if (await roleManager.FindByNameAsync(adminName) == null)
                await roleManager.CreateAsync(new IdentityRole(adminName));
            
            if (await roleManager.FindByNameAsync("customer") == null)
                await roleManager.CreateAsync(new IdentityRole("customer"));//Заказчик
            
            if (await roleManager.FindByNameAsync("executor") == null)
                await roleManager.CreateAsync(new IdentityRole("executor"));//Исполнитель
            
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = Properties.CompanyEmail,
                    UserName = adminName,
                    Role = adminName,
                    PhoneNumber = Properties.CompanyPhone,
                    FullName = Properties.CompanyName,
                    Description = "Этот аккаунт предназначен для управления всей системой"
                };
                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
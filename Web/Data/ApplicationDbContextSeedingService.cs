namespace SimonSampleApp.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContextSeedingService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppDbContextSeedingService(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task EnsureMigratedAndSeededAsync()
        {
            await _dbContext.Database.MigrateAsync().ConfigureAwait(false);

            if (await _dbContext.Roles.AnyAsync().ConfigureAwait(false))
                return;

            await AddRolesAndUsersAsync().ConfigureAwait(false);
        }

        private async Task AddRolesAndUsersAsync()
        {
            await Task.WhenAll(
                    _roleManager.CreateAsync(new IdentityRole { Name = AppDbContext.AdminRoleName }),
                    _roleManager.CreateAsync(new IdentityRole { Name = AppDbContext.DateOperatorRoleName }))
                .ConfigureAwait(false);
            
            await Task.WhenAll(
                    AddUserAsync("admin", AppDbContext.AdminRoleName),
                    AddUserAsync("data_operator", AppDbContext.DateOperatorRoleName))
                .ConfigureAwait(false);

            async Task AddUserAsync(string id, string roleName)
            {
                var email = $"{id}@email.com";
                var user = new IdentityUser
                {
                    Id = id,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, "Pa55w0rd!").ConfigureAwait(false);
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}")));

                await _userManager.AddToRoleAsync(user, roleName).ConfigureAwait(false);
            }
        }
    }
}
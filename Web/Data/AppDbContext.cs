namespace SimonSampleApp.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext
    {
        public const string AdminRoleName = "Admin";
        public const string DateOperatorRoleName = "DataOperator";
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
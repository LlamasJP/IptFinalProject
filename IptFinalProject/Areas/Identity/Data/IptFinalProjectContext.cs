using IptFinalProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IptFinalProject.Models;

namespace IptFinalProject.Data;

public class IptFinalProjectContext : IdentityDbContext<IptFinalProjectUser>
{
    public IptFinalProjectContext(DbContextOptions<IptFinalProjectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<IptFinalProject.Models.PersonalInfoModel> PersonalInfo { get; set; } = default!;
}

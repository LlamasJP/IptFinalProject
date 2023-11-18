using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IptFinalProject.Data;
using IptFinalProject.Areas.Identity.Data;
namespace IptFinalProject.Controllers
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("IptFinalProjectContextConnection") ?? throw new InvalidOperationException("Connection string 'IptFinalProjectContextConnection' not found.");

            builder.Services.AddDbContext<IptFinalProjectContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IptFinalProjectUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IptFinalProjectContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Staff", "Student" };

                foreach (var role in roles)
                {

                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<IptFinalProjectUser>>();

                string email = "admin@admin.com";
                string password = "Abc@123";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IptFinalProjectUser();
                    user.UserName = email;
                    user.Email  = email;
                    user.FirstName = "Admin";
                    user.LastName = "Admin";



                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            app.Run();
        }
    }
}
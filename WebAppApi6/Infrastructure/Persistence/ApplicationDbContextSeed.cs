//using System.Linq;
//using System.Threading.Tasks;
//using Domain.Entities;
//using Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;

//namespace Infrastructure.Persistence
//{
//    public static class HostExtensions
//    {
//        /// <summary>
//        /// Seed database with sample data.
//        /// </summary>
//        /// <param name="host">Host builder.</param>
//        public static async Task SeedDatabaseAsync(this IHost host)
//        {
//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;

//                try
//                {
//                    var context = services.GetRequiredService<ApplicationDbContext>();

//                    if (context.Database.IsSqlServer())
//                    {
//                        context.Database.Migrate();
//                    }

//                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
//                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);
//                }
//                catch (Exception ex)
//                {
//                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

//                    throw;
//                }
//            }
//        }
//    }
//    /// <summary>
//    /// Define extensions to seed Application context with sample data.
//    /// </summary>
//    public static class ApplicationDbContextSeed
//    {
//        /// <summary>
//        /// Add default users to Application DB context.
//        /// </summary>
//        /// <param name="userManager">Service to manage users.</param>
//        /// <param name="roleManager">Service to manage user roles.</param>
//        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            var administratorRole = new IdentityRole("Administrator");

//            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
//            {
//                await roleManager.CreateAsync(administratorRole);
//            }

//            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

//            if (userManager.Users.All(u => u.UserName != administrator.UserName))
//            {
//                await userManager.CreateAsync(administrator, "Administrator1!");
//                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
//            }
//        }

//        /// <summary>
//        /// Add sample data to Application DB context.
//        /// </summary>
//        /// <param name="context">Application DB context.</param>
//        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
//        {
//            // Seed, if necessary
//            if (!context.TodoLists.Any())
//            {
//                context.TodoLists.Add(new TodoList
//                {
//                    Title = "Shopping",
//                    //Colour = Colour.Blue,
//                    Items =
//                    {
//                        new TodoItem { Title = "Apples", Done = true },
//                        new TodoItem { Title = "Milk", Done = true },
//                        new TodoItem { Title = "Bread", Done = true },
//                        new TodoItem { Title = "Toilet paper" },
//                        new TodoItem { Title = "Pasta" },
//                        new TodoItem { Title = "Tissues" },
//                        new TodoItem { Title = "Tuna" },
//                        new TodoItem { Title = "Water" }
//                    }
//                });

//                await context.SaveChangesAsync();
//            }
//        }
//    }
//}
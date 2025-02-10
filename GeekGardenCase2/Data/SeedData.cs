using GeekGardenCase2.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase2.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<AppDbContext>>());

            if(context.Users.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "Manager" },
                new Role { Name = "Employee" }
            };

            var dept = new Department[]
            {
                new Department { Name = "IT" },
                new Department { Name = "Accountant" },
                new Department { Name = "Marketing" }
            };

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(dept);
                context.SaveChanges();
            }

            //seeding
            var adminRole = context.Roles.Single(r => r.Name == "Admin");
            var managerRole = context.Roles.Single(r => r.Name == "Manager");
            var employeeRole = context.Roles.Single(r => r.Name == "Employee");

            var itDept = context.Departments.Single(r => r.Name == "IT");
            var accDept= context.Departments.Single(r => r.Name == "Accountant");
            var marketDept= context.Departments.Single(r => r.Name == "Marketing");

            var users = new[]
            {
                new User { Username = "admin", Password = HashPassword("adminpassword"), Role = adminRole},
                new User { Username = "manager1", Password = HashPassword("managerpassword"), 
                    Role = managerRole, Department= itDept},
                new User { Username = "manager2", Password = HashPassword("managerpassword"), 
                    Role = managerRole, Department=accDept},
                new User { Username = "manager3", Password = HashPassword("managerpassword"), 
                    Role = managerRole, Department=marketDept},
                
                new User { Username = "emp1", Password = HashPassword("emppassword"), 
                    Role = employeeRole,Department= itDept},
                new User { Username = "emp2", Password = HashPassword("emppassword"), 
                    Role = employeeRole,Department= accDept},
                new User { Username = "emp3", Password = HashPassword("emppassword"), 
                    Role = employeeRole,Department= marketDept}
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

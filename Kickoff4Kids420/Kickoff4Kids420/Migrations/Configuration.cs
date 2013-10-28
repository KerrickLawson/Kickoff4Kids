using System.Web.Security;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace Kickoff4Kids420.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Kickoff4Kids420.Models.Kickoff4KidsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Kickoff4Kids420.Models.Kickoff4KidsDb context)
        {

            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName",
                autoCreateTables: true);

            var roles = (SimpleRoleProvider) Roles.Provider;
            var membership = (SimpleMembershipProvider) Membership.Provider;
            // Admin Role
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("Admin", false) == null)
            {
                membership.CreateUserAndAccount("Admin", "123");
            }
            if (!roles.GetRolesForUser("Admin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new []{"Admin"}, new []{"admin"});
            }

            // Teacher Role
            if (!roles.RoleExists("Teacher"))
            {
                roles.CreateRole("Teacher");
            }
            if (membership.GetUser("Teacher1", false) == null)
            {
                membership.CreateUserAndAccount("Teacher1", "123");
            }
            if (!roles.GetRolesForUser("Teacher1").Contains("Teacher"))
            {
                roles.AddUsersToRoles(new []{"Teacher1"}, new []{"teacher"});
            }


        }
    }
}

using System.Web.Security;
using Kickoff4Kids420.Models;
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

            
              //This method will be called after migrating to the latest version.

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.

            context.FrontPage.AddOrUpdate(
              
              new FrontPage
              {
                  MainText = "Are you interested in earning cool rewards while having fun? There's no time to waste!",
                  MainButton = "Join the Competition!",
                  HeaderOneText = "Academics",
                  HeaderOneComment = "By excelling in the classroom students will have the opportunity to accumulate points and earn rewards based of their academic accomplishments!",
                  HeaderTwoText = "Extracurricular Activities",
                  HeaderTwoComment = "Get involved with extracurricular activities at your school and earn points while you having fun!",
                  HeaderThreeText = "Community Service",
                  HeaderThreeComment = "Contribute to your community and earn points at the same time! Volunteer your services and gain experiences that will last a lifetime!"
              }
            );
            
            SeedMembership();
        }
        private void SeedMembership()
        {
            //Kickoff4Kids420.Models.Kickoff4KidsDb db = new Kickoff4Kids420.Models.Kickoff4KidsDb();

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
                membership.CreateUserAndAccount("Admin", "123456");
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
                membership.CreateUserAndAccount("Teacher1", "123456");
            }
            if (!roles.GetRolesForUser("Teacher1").Contains("Teacher"))
            {
                roles.AddUsersToRoles(new []{"Teacher1"}, new []{"teacher"});
            }
            //// Student Role
            //if (!roles.RoleExists("Student"))
            //{
            //    roles.CreateRole("Student");
            //}
            //if (membership.GetUser("Student1", false) == null)
            //{
            //    membership.CreateUserAndAccount("Student1", "123456");
                
            //}
            //if (!roles.GetRolesForUser("Student1").Contains("student"))
            //{

            //    //var studentID =
            //    //    from s in db.UserProfiles
            //    //    where s.UserName == "Student1"
            //    //    select s.UserId;

            //    roles.AddUsersToRoles(new[] { "Student1" }, new[] { "student" });
            //    //db.UserProfiles student1 = new db.user
                
            //    //db.UserProfiles.AddOrUpdate(studentID,db.UserProfiles,);

            //}


        }
    }
}

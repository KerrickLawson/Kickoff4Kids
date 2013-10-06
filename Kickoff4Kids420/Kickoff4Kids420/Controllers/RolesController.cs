using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    public class RolesController : Controller
    {
        //
        

        public ActionResult Index()
        {

            return View();
        }

         
        
        public ActionResult Users()
        {
            IEnumerable<UserProfile> users;
            using (var db = new Kickoff4KidsDb())
            {
                users = db.UserProfiles.ToList();
            }

            ViewBag.Roles = System.Web.Security.Roles.GetAllRoles();
            
            return View(users);
        }
       
        public ActionResult Roles()
        {
            var roles = System.Web.Security.Roles.GetAllRoles();

            return View(roles);
        }

        [HttpPost]
        public ActionResult AddRole(string rolename)
        {
            System.Web.Security.Roles.CreateRole(rolename);
            var roles = System.Web.Security.Roles.GetAllRoles();

            return View("Roles", roles);
        }
        [HttpPost]
        public ActionResult UserToRole(string rolename, string username, bool? ischecked)
        {
            if (ischecked.HasValue && ischecked.Value)
            {
                System.Web.Security.Roles.AddUserToRole(username, rolename);
            }
            else
            {
                System.Web.Security.Roles.RemoveUserFromRole(username, rolename);
            }
            
            return RedirectToAction("Users");
        }
    }
}

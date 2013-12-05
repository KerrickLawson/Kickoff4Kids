using System.Collections.Generic;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kickoff4Kids420.Models;
using PagedList;
using PagedList.Mvc;

namespace Kickoff4Kids420.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        //
        

        public ActionResult Index()
        {

            return View();
        }



        public ActionResult Users(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<UserProfile> user;
            using (var db = new Kickoff4KidsDb())
            {
                 user = from u in db.UserProfiles.ToList()
                            select u;

                if (!String.IsNullOrEmpty(searchString))
                {
                    user = user.Where(u => u.UserName.ToUpper().Contains(searchString.ToUpper()));

                }
                //users = db.UserProfiles.ToList();

            }

            ViewBag.Roles = System.Web.Security.Roles.GetAllRoles();

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(user.ToPagedList(pageNumber, pageSize));
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

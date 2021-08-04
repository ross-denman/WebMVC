using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Data;

namespace WebMVC.Controllers
{
    public class NameController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var query = context.Users
                                 .Where(d => d.UserName == username)
                                 .Select(d => d.DisplayName)
                                 .FirstOrDefault();
                    //string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    string profileName = query;
                    ViewData.Add("ProfileName", profileName);
                }
            }
            base.OnActionExecuted(filterContext);
        }
        public NameController()
        { }
    }
}

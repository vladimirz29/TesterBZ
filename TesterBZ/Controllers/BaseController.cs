using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterBZDomainModel;
using TesterBZDomainModel.Models;

namespace TesterBZ.Controllers
{
    public class BaseController : Controller
    {
        public TesterBZContext Context { get; set; }

        ApplicationUserManager _userManager;
        protected ApplicationUserManager UserManager => _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());

        public ApplicationUser UserProfile
        {
            get
            {
                return User.Identity.IsAuthenticated ? UserManager.FindByName(User.Identity.Name) : null;
            }
        }

        public BaseController()
        {
            Context = new TesterBZContext();
        }
    }
}
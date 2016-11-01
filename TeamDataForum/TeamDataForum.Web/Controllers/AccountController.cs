using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamDataForum.DB;
using TeamDataForum.Web.Models;

namespace TeamDataForum.Web.Content
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        public ActionResult SignUp(UserLog user)
        {
            TeamDataForumContext db = new TeamDataForumContext();
            db.Countries.Count();
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserLog user)
        {
            return View();
        }
    }
}
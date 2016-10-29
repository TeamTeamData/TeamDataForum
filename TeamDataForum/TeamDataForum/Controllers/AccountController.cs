using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamDataForum.Models;

namespace TeamDataForum.Content
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        public ActionResult SignUp(UserLog user)
        {
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
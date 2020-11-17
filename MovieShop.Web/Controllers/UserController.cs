using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class UserController : Controller
    {
        // HTTP status code 404
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Create users, return status of insertion 
        //[HttpPost]
        public IActionResult Create(/*User user*/)
        {
            return View();
        }
        // Get user details
        [HttpGet]
        public IActionResult Details(int userID)
        {
            return View();
        }
    }
}

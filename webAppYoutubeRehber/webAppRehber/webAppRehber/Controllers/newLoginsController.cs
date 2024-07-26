using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webAppRehber.Data;
using webAppRehber.Models;

namespace webAppRehber.Controllers
{

    public class newLoginsController : Controller
    {
        private readonly DBContext _context;

        public newLoginsController(DBContext context)
        {
            _context = context;
        }

        // GET: newLogins
        public async Task<IActionResult> Index()
        {
            return View(await _context.newLogins.ToListAsync());
        }



        // GET: newLogins/Create

        public IActionResult Create()
        {
            return View();
        }



        public IActionResult SıgIn()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,Password")] newLogin newLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction("SıgIn", "newLogins");
            }
            return View(newLogin);
        }



        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }






		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequest model)
		{
			if (ModelState.IsValid)
			{
				string loginName = model.Username; // Get username from the model
				string password = model.Password; // Get password from the model

				// Check user credentials in the database
				var user = _context.newLogins.FirstOrDefault(u => u.Email == loginName);

				// If user is found and password matches
				if (user != null && user.Password == password)
				{
					// Set a cookie
					var cookieOptions = new CookieOptions
					{
						HttpOnly = true,
						Expires = DateTime.UtcNow.AddHours(1) // Set expiration time for the cookie
					};
					Response.Cookies.Append("IsLoggedIn", "Değer Döndü", cookieOptions);

					return Ok("Giriş başarılı!");
				}

				// Return error if login credentials are invalid
				return BadRequest("Geçersiz giriş bilgileri.");
			}

			// Return error if model state is invalid
			return BadRequest("Geçersiz model.");
		}
        [Authorize]
		[HttpGet]
		public IActionResult ProtectedPage()
		{
			if (!Request.Cookies.ContainsKey("IsLoggedIn"))
			{
				return Redirect("/login");
			}

			// Your protected content
			return View();
		}
	}
}
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webAppRehber.Data;
using webAppRehber.Models;

namespace webAppRehber.Controllers
{


    public class newRehbersController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public newRehbersController(DBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
      

       

        // GET: newRehbers   

        public async Task<IActionResult> Index()
        {
            return View(await _context.newRehbers.ToListAsync());
        }

        //Get newRehbers/SearchForm

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }



       
        //Get Login Page
        public async Task<IActionResult> Login()
        {
            return View();
        }

        



        //Post newRehbers/SearchForm
        public async Task<IActionResult> ShowSearchResults(string searchPhrase)
		{
			var results = await _context.newRehbers
										.Where(x => x.YeniKişiAdı.Contains(searchPhrase))
										.ToListAsync();

			return View("Index", results);
		}

  
		// GET: newRehbers/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newRehber = await _context.newRehbers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newRehber == null)
            {
                return NotFound();
            }

            return View(newRehber);
        }


        // GET: newRehbers/Create


        
        public IActionResult Create()
        {
            return View();
        }

        // POST: newRehbers/Create
  
        [HttpPost]
        public async Task<IActionResult>Create(newRehber rehber, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];

                    if (file.Length > 0)
                    {
                        // GUID kullanarak benzersiz bir dosya adı oluşturun
                        var fileExtension = Path.GetExtension(file.FileName); // Dosya uzantısını alın
                        var fileName = $"{Guid.NewGuid()}{fileExtension}"; // GUID ve uzantıyı birleştirin
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images", fileName);

                        // Dosyayı kaydet
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);

                        }
                        // Dosya yolunu veritabanına kaydedin
                        rehber.photopath = "images/" + fileName;
                    }
                }

                // Person nesnesini veritabanına kaydetme işlemleri
                _context.Add(rehber);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(rehber);
        }


        // GET: newRehbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newRehber = await _context.newRehbers.FindAsync(id);
            if (newRehber == null)
            {
                return NotFound();
            }
            return View(newRehber);
        }

        // POST: newRehbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.  
        // GET: newRehbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newRehber = await _context.newRehbers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newRehber == null)
            {
                return NotFound();
            }

            return View(newRehber);
        }

        // POST: newRehbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newRehber = await _context.newRehbers.FindAsync(id);
            if (newRehber != null)
            {
                _context.newRehbers.Remove(newRehber);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool newRehberExists(int id)
        {
            return _context.newRehbers.Any(e => e.ID == id);
        }
        [HttpPost]
        
        public IActionResult Logout()
        {
          
           
            // Delete the authentication cookie
            if (Request.Cookies.ContainsKey("IsLoggedIn"))
            {
                Response.Cookies.Delete("IsLoggedIn");
                return RedirectToAction("SıgIn", "newLogins");
            }

            // Redirect to the login page
           return Ok();
        }

        public class AuthenticationMiddleware
        {
            private readonly RequestDelegate _next;

            public AuthenticationMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                if (!context.Request.Cookies.ContainsKey("IsLoggedIn"))
                {
                    // Redirect to login page if the cookie is not present
                    context.Response.Redirect("/login");
                    return;
                }

                await _next(context);
            }
        }
    }
}


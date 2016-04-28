using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FlickrClone.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity;
using FlickrClone.ViewModels;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNet.Mvc.Rendering;
using System.Security.Claims;

namespace FlickrClone.Controllers
{
    public class PictureController : Controller
    {
        //private ApplicationDbContext _db = new ApplicationDbContext();

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private IHostingEnvironment _environment;

        public PictureController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>
           signInManager, ApplicationDbContext db, IHostingEnvironment environment)
        {
                _environment = environment;
                _userManager = userManager;
                _signInManager = signInManager;
                _db = db;
        }

        public IActionResult Index()
        {

            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    await file.SaveAsAsync(Path.Combine(uploads, fileName));
                    //instantiate new picture object called "newPic"


                    Picture newPic = new Picture();
                    //assign newPic property pictureUrl to uploads and filename var containg url
                    newPic.PictureURL = "/uploads/" + fileName;
                    //hard code sample entry
                    //use request form to get newpic by categoryId
                    newPic.CategoryId = Int32.Parse(Request.Form["CategoryId"]);
                    var user = await _userManager.FindByIdAsync(User.GetUserId());
                    newPic.User = user;
                    _db.Pictures.Add(newPic);
                    _db.SaveChanges();

                }
            }     
            return RedirectToAction("index");
        }
    }
}

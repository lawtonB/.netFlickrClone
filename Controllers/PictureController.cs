﻿using System;
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

namespace FlickrClone.Controllers
{
    public class PictureController : Controller
    {
       private ApplicationDbContext _db = new ApplicationDbContext();

        private IHostingEnvironment _environment;

        public PictureController(IHostingEnvironment environment)
        {
            _environment = environment;
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
                    //int newId = Int32.Parse(id);
                    newPic.CategoryId = Int32.Parse(Request.Form["CategoryId"]);
                    _db.Pictures.Add(newPic);
                    _db.SaveChanges();

                }
            }     
            return RedirectToAction("index");
        }
    }
}

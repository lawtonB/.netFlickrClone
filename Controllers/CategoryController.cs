using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FlickrClone.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity;
using FlickrClone.ViewModels;
using Microsoft.AspNet.Authorization;

namespace FlickrClone.Controllers
{
    public class CategoryController : Controller
    {

        private ApplicationDbContext _db = new ApplicationDbContext();

        public IActionResult Index()
        {

            return View(_db.Categories.ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory(string category)
        {
            Category newCategory = new Category();
            newCategory.CategoryName = category;
           _db.Categories.Add(newCategory);
           _db.SaveChanges();
           return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            _db.Categories.Remove(thisCategory);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult CategoryDetails(int id)
        {
            return View(_db.Pictures.Where(x => x.CategoryId == id).ToList());
        }
    }

}

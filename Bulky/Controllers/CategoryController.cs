using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category) 
        {
            //if (category.Name == category.displayOrder.ToString())
            //{
            //    ModelState.AddModelError("", "Name & DO can't be the same");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            //if different controller write return RedirectToAction("Index", "name of the controller");
        }

        public IActionResult Edit(int id)
        {
            Category? foundCategory = _db.Categories.Find(id); //find only work for primary key
            //Category? foundCategory1 = _db.Categories.FirstOrDefault(u => u.CategoryId == id); //other way to get a category with specific id
            return View(foundCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Category? foundCategory = _db.Categories.Find(id);
            if (foundCategory == null)
            {
                return NotFound();
            } 
            _db.Categories.Remove(foundCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using Bulky.DataAccess.Repository.IRepository;
using Bulky.DtaAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _categoryRepository.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
            //it can be written like that
            //return View(new Category);
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
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
            //if different controller write return RedirectToAction("Index", "name of the controller");
        }

        public IActionResult Edit(int id)
        {
            Category? foundCategory = _categoryRepository.Get(u => u.CategoryId == id);
            return View(foundCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Category foundCategory = _categoryRepository.Get(u => u.CategoryId == id);
            TempData["success"] = "Category deleted successfully";
            _categoryRepository.Remove(foundCategory);
            _categoryRepository.Save();
            return RedirectToAction("Index");
        }
    }
}

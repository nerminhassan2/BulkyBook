using Bulky.DataAccess.Repository.IRepository;
using Bulky.DtaAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
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
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
            //if different controller write return RedirectToAction("Index", "name of the controller");
        }

        public IActionResult Edit(int id)
        {
            Category? foundCategory = _unitOfWork.CategoryRepository.Get(u => u.CategoryId == id);
            return View(foundCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Category foundCategory = _unitOfWork.CategoryRepository.Get(u => u.CategoryId == id);
            TempData["success"] = "Category deleted successfully";
            _unitOfWork.CategoryRepository.Remove(foundCategory);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}

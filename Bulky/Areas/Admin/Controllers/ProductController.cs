using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() 
        { 
            List<Product> products = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().
                    Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()
                    }),

                Product = new Product()
            };
            return View(productVM);

            //CategoryList is temporary data that is not in a model so I can't pass it directly to the View()!!!
            //ViewBag transfer data from controller to view
            //ViewBag.CategoryList = CategoryList;

            //ViewData["CategoryList"] = CategoryList;
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().
                    Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()
                    });

                return View(productVM);
            }

        }

        public IActionResult Edit(int id)
        {
            Product foundProduct = _unitOfWork.ProductRepository.Get(u => u.ProductId == id);
            return View(foundProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product foundProduct = _unitOfWork.ProductRepository.Get(u => u.ProductId == id);
            _unitOfWork.ProductRepository.Remove(foundProduct);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }


    }
}

using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Upsert(int? id)
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

            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.ProductRepository.Get(u => u.ProductId == id);
                return View(productVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
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

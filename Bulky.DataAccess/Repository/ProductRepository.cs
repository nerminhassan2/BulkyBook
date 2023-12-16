using Bulky.DataAccess.Repository.IRepository;
using Bulky.DtaAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Update(obj);    
            //var objFromDb = _db.Products.FirstOrDefault(u => u.ProductId == obj.ProductId);
            //if (objFromDb != null)
            //{
            //    objFromDb.Title = obj.Title;
            //    objFromDb.ISBN = obj.ISBN;
            //    objFromDb.Price = obj.Price;
            //    objFromDb.Price50 = obj.Price50;
            //    objFromDb.ListPrice = obj.ListPrice;
            //    objFromDb.Price100 = obj.Price100;
            //    objFromDb.Description = obj.Description;
            //    objFromDb.CategoryID = obj.CategoryID;
            //    objFromDb.Author = obj.Author;
            //    if (obj.ImageURL != null)
            //    {
            //        objFromDb.ImageURL = obj.ImageURL;
            //    }
            //}
        }
    }
}

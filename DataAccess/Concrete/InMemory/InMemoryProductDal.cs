using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductID=1,
                    CategoryID=1,
                    ProductName="Bardak",
                    UnitPrice=15,
                    UnitsInStock=1540
                },
                new Product
                {
                    ProductID=2,
                    CategoryID=1,
                    ProductName="Mouse",
                    UnitPrice=200,
                    UnitsInStock=150
                },
                new Product
                {
                    ProductID=3,
                    CategoryID=2,
                    ProductName="Kilim",
                    UnitPrice=9,
                    UnitsInStock=1500
                },
                new Product
                {
                    ProductID=4,
                    CategoryID=2,
                    ProductName="Bilgisayar",
                    UnitPrice=19,
                    UnitsInStock=15100
                },
                new Product
                {
                    ProductID=5,
                    CategoryID=2,
                    ProductName="Monitor",
                    UnitPrice=119,
                    UnitsInStock=100
                },

            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryID == categoryId).ToList();
             
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == p.ProductID);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryID = product.CategoryID;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}

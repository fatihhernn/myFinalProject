using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Businness;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }





        //ENTITY MANAGER KENDISI HARIC BASKA BIR DAL'I ENJECTE EDEMEZ. BURAYA SADECE PRODUCTDAL ENJECTE EDILIR



        //[ValidationAspect(typeof(ProductValidator))] =>attribute => metoda anlam katıyor burada add metoduna senin bir validation aspect var diyoruz onu da AUTOFAC devreye sokuyor

        //claim => iddia "admin", "editor" bir claim'dir.
        //[SecuredOperation("product.add,admin")]

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            #region CodeSmell_1
            //bir kategoride en fazla 10 ürün bulunabilir.
            //int result = _productDal.GetAll(p => p.CategoryID == product.CategoryID).Count;
            //if (result>=10)
            //{
            //    return new ErrorResult(Messages.ProductCountError);
            //}
            #endregion

            #region CodeSmell_2
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryID).Success)
            //{
            //    if (CheckIfProductNameExist(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }               
            //}
            //return new ErrorResult();
            #endregion

            //iş kurallarını parametre olarak gönder

            IResult result = BusinessRules.Run(
                    CheckIfProductNameExist(product.ProductName),
                    CheckIfProductCountOfCategoryCorrect(product.CategoryID),
                    CheckIfCategoryLimitExcced()
                    );

            //kurala uymayan paramatre dönerse 
            if (result!=null)
            {
                return result; //yeni hata dönecek ve iş kuralı başarısız olacak
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }


        [CacheAspect] //key,value  => "key" cache verdiğimiz isim,"value"
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new DataResult<List<Product>>(_productDal.GetAll(), true,Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return  new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryID==id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour==12)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

       // [ValidationAspect(typeof(ProductValidator))]
       [CacheRemoveAspect("IProductService.Get")]//içerisinde get geçenleri sil
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        //iş kuralı parçacığı sadece burada kullancam.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)//Product product olarakta gönderebilirsin.
        {

            //select count(*) from products where CategoryId=1 => bu query database gönderilir önce, hepsini çekip bize data göstermez 
            int result = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountError);
            }
            return new SuccessResult();
        }


        private IResult CheckIfProductNameExist(string productName)//Product product olarakta gönderebilirsin.
        {

            //select count(*) from products where CategoryId=1 => bu query database gönderilir önce, hepsini çekip bize data göstermez 
            bool result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        //product için kategori service nasıl yorumlanıyor ? 
        //burada servisi injecte edip onun yorumladık
        private IResult CheckIfCategoryLimitExcced()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count>30)
            {
                return new ErrorResult(Messages.CategoryLimitExceed);
            }
            return new SuccessResult();
        }
    }
}

using Northwind.Business.Abstract;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using Nortwind.Business.Utilities;
using Nortwind.Business.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{

    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private EfCategoryDal efCategoryDal;

        public ProductManager(IProductDal productDal)//dependest injection
        {
            _productDal = productDal;  //construcher
        }

        public ProductManager(EfCategoryDal efCategoryDal)
        {
            this.efCategoryDal = efCategoryDal;
        }

        public void Add(Product product)
        
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Add(product);
        }

        private static IList<FluentValidation.Results.ValidationFailure> GetErrors(FluentValidation.Results.ValidationResult result)
        {
            return result.Errors;
        }



        public void Delete(Product product)
        {
            try
            {

                _productDal.Delete(product);

            }
            catch //(DbUpdateException e)
            {
                throw new Exception("Silme gerçekleşmedi.");
            }
        }

        //  EfProductDal _productDal = new EfProductDal();
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetProductByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Update(product);

        }
    }
}

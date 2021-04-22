using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.DataAccesLayer;
using Northwind.DataAccesLayer.Abstract;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;
using System.Linq;

namespace Northwind.Test
{
    /// <summary>
    /// Repository ve UOW kullanarak ilgili test metotlarımızı yazıyoruz.
    /// </summary>
    [TestClass]
    public class EntityTest
    {
        // Entity framework için geliştirmiş olduğumuz context. Farklı ORM veya Veritabanı içinde bu context'i değiştirebiliriz.
        private DataContext _dbContext;
        private EFUnitOfWork _uow;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new DataContext();

            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.

            _uow = new EFUnitOfWork();
           
        
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext = null;
            _uow.Dispose();
        }


        [TestMethod]
        public void AddEmloyee()
        {
            Category employee = new Category
            {
                CategoryName = "a",
                Description = "a"
            };

            _uow.GetRepository<Category>().CreateOrUptade(employee);
            _uow.Save();

            var model = _dbContext.Categories.Find(employee.CategoryID);

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void GetEmployee()
        {
            Category employee = _uow.GetRepository<Category>().GetById(19);

            Assert.IsNotNull(employee);
        }

        [TestMethod]
        public void UptadeEmployee()
        {
            Category employee = _uow.GetRepository<Category>().GetById(19);

            employee.CategoryName = "Uptade";

            _uow.GetRepository<Category>().CreateOrUptade(employee);
            _uow.Save();

            //Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void DeleteEmployee()
        {


            _uow.GetRepository<Category>().DeleteById(19);
            _uow.Save();

            //Assert.AreNotEqual(-1, process);
        }
    }
}

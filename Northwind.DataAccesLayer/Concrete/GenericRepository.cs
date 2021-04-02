using Northwind.DataAccesLayer.Abstract;
using Northwind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.DataAccesLayer.Concrete
{
    public class GenericRepository<T, Context>: IRepository<T> where T: class
    {

        private DataContext _context;
        private DbSet<T> _table;

        public GenericRepository()
        {
            _context = new DataContext();

            _table = _context.Set<T>();
        }

        public void CreateOrUptade(T model)
        {
            _table.AddOrUpdate(model);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            _table.Remove(_table.Find(id));
            _context.SaveChanges();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                         ? _context.Set<T>().ToList()
                         : _context.Set<T>().Where(filter).ToList();
        }

        public virtual List<T> GetList()
        {
           return _table.ToList();
        }
    }

}

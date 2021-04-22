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
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        public DataContext _context;
        private readonly DbSet<T> _table;

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
          
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                _context.Dispose();
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                         ? _table.ToList()
                         : _table.Where(filter).ToList();
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public List<T> GetList()
        {
            return _table.ToList();
        }
    }

}

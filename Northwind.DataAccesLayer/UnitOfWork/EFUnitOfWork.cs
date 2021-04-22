using Northwind.DataAccesLayer.Abstract;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;
using System;
using System.Data.Entity;

namespace Northwind.DataAccesLayer
{
    //public class EFUnitOfWork : IUnitOfWork
    //{
    //    private readonly DataContext _dbContext;

    //    public EFUnitOfWork(DataContext dbContext)
    //    {
    //        Database.SetInitializer<DataContext>(null);

    //        if (dbContext == null)
    //            throw new ArgumentNullException("dbContext can not be null.");

    //        _dbContext = dbContext;

    //        // Buradan istediğiniz gibi EntityFramework'ü konfigure edebilirsiniz.
    //        //_dbContext.Configuration.LazyLoadingEnabled = false;
    //        //_dbContext.Configuration.ValidateOnSaveEnabled = false;
    //        //_dbContext.Configuration.ProxyCreationEnabled = false;
    //    }

    //    #region IUnitOfWork Members
    //    public IRepository<T> GetRepository<T>() where T : class
    //    {
    //        return new GenericRepository<T>();
    //    }

    //    public int SaveChanges()
    //    {
    //        try
    //        {
    //            return _dbContext.SaveChanges();
    //        }
    //        catch
    //        {
    //            // Burada DbEntityValidationException hatalarını handle edebiliriz.
    //            throw;
    //        }
    //    }
    //    #endregion

    //    #region IDisposable Members
    //    // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
    //    private bool disposed = false;
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                _dbContext.Dispose();
    //            }
    //        }

    //        this.disposed = true;
    //    }
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    #endregion
    //}

    public class EFUnitOfWork
    {
        private readonly DataContext _context;
        public EFUnitOfWork()
        {
            _context = new DataContext();
        }
        
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>();
        }


        public void Save()
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO:Logging
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

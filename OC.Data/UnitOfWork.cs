using OC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly OnlineCosmeticsDBContext dbContext;
        private BaseRepository<Product> productRepository;
        private BaseRepository<Brand> brandRepository;
        private BaseRepository<Category> categoryRepository;
        private bool disposed = false;

        public UnitOfWork()
        {
            this.dbContext = new OnlineCosmeticsDBContext();
            //dbContext.Database.EnsureCreated();
        }

        public BaseRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new BaseRepository<Product>(dbContext);
                }

                return productRepository;
            }
        }

        public BaseRepository<Brand> BrandRepository
        {
            get
            {
                if (this.brandRepository == null)
                {
                    this.brandRepository = new BaseRepository<Brand>(dbContext);
                }

                return brandRepository;
            }
        }

        public BaseRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new BaseRepository<Category>(dbContext);
                }

                return categoryRepository;
            }
        }

        public bool Save()
        {
            try
            {
                dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposed = true;
            }
        }
    }
}

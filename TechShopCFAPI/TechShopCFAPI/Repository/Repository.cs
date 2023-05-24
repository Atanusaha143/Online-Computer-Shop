using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected TechShopDbContext context = new TechShopDbContext();


        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }
        public void Delete(int id)
        {
            context.Set<TEntity>().Remove(context.Set<TEntity>().Find(id));
            context.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
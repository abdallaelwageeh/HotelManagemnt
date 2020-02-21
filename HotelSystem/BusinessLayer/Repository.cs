using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelSystem.BusinessLayer
{
    public class Repository<TEntity, TIdType> : IRepository<TEntity, TIdType> where TEntity : class
    {
        DbSet<TEntity> Data;
        HotelDbContext Context;
        public Repository(HotelDbContext context)
        {
            Data = context.Set<TEntity>();
            Context = context;
        }
        public bool Delete(TIdType Id)
        {
            var entity = Data.Find(Id);
            if (entity != null)
            {
                Data.Remove(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        public IQueryable<TEntity> GetAll()
        {
            return Data;
        }
        public List<TEntity> GetAllToList()
        {
            return GetAll().ToList();
        }
        public TEntity GetById(TIdType Id)
        {
            return Data.Find(Id);
        }
        public bool Update(TEntity E)
        {
            if (E != null)
            {
                Data.Attach(E);
                Context.Entry(E).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Insert(TEntity E)
        {
            if (E != null)
            {
                Data.Add(E);
                return true;
            }
            return false;
        }
    }
}
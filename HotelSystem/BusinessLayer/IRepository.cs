using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.BusinessLayer
{
    interface IRepository <TEntity, TIdType>
    {
        IQueryable<TEntity> GetAll();
        List<TEntity> GetAllToList();
        TEntity GetById(TIdType Id);
        bool Update(TEntity E);
        bool Insert(TEntity E);
        bool Delete(TIdType Id);
    }
}

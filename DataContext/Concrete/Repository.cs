using DataContext.Abstract;
using Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Concrete
{
    public class Repository : IRepository<BaseEntity>
    {
        private readonly ContextDb _contextDb;

        public Repository(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        public void Add(BaseEntity data)
        {
            _contextDb.Add(data);
            _contextDb.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetByData(x=>x.Id==id);
            _contextDb.Remove(entity);
            _contextDb.SaveChanges();
        }

        public List<BaseEntity> GetAllDatas()
        {
            return _contextDb.Set<BaseEntity>().ToList();   
        }

        public BaseEntity GetByData(Expression<Func<BaseEntity, bool>> expression)
        {
            return _contextDb.Set<BaseEntity>().FirstOrDefault(expression);
        }

        public void Update(BaseEntity data)
        {
            _contextDb.Set<BaseEntity>().Update(data);
            _contextDb.SaveChanges();
        }
    }
}

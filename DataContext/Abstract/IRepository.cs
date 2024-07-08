using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Abstract
{
    public interface IRepository<T> where T : class
    {
        T GetByData(Expression<Func<T, bool>> expression);
        List<T> GetAllDatas();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}

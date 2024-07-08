using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAllDatas();
        T GetByData(Expression<Func<T, bool>> expression);
        void Add(T data);
        void Update(T data);
        void Delete(int id);
    }
}

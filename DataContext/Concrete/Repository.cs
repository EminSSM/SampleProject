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
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly ContextDb _contextDb;

		public Repository(ContextDb contextDb)
		{
			_contextDb = contextDb;
		}

		public void Add(T entity)
		{
			_contextDb.Set<T>().Add(entity);
			_contextDb.SaveChanges();
		}

		public void Delete(int id)
		{
			var entity = GetByData(x => x.Id == id);
			if (entity != null)
			{
				_contextDb.Set<T>().Remove(entity);
				_contextDb.SaveChanges();
			}
		}

		public void Delete(T entity)
		{
			_contextDb.Set<T>().Remove(entity);
			_contextDb.SaveChanges();
		}

		public List<T> GetAllDatas()
		{
			return _contextDb.Set<T>().ToList();
		}

		public T GetByData(Expression<Func<T, bool>> expression)
		{
			return _contextDb.Set<T>().FirstOrDefault(expression);
		}

		public void Update(T entity)
		{
			_contextDb.Set<T>().Update(entity);
			_contextDb.SaveChanges();
		}
	}
}

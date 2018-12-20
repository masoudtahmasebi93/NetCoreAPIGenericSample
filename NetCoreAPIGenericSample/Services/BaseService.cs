using Microsoft.EntityFrameworkCore;
using NetCoreAPIGenericSample.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreAPIGenericSample.Services
{
    public class BaseService<TObject> where TObject : class
    {
        protected DataContext _context;

        public BaseService(DataContext context)
        {
            _context = context;
        }

        public ICollection<TObject> GetAll()
        {
            return _context.Set<TObject>().ToList();
        }

        public async Task<ICollection<TObject>> GetAllAsync()
        {
            return await _context.Set<TObject>().ToListAsync();
        }

        public TObject Get(long id)
        {
            return _context.Set<TObject>().Find(id);
        }

        public async Task<TObject> GetAsync(long id)
        {
            return await _context.Set<TObject>().FindAsync(id);
        }

        public TObject Find(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().SingleOrDefault(match);
        }

        public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().SingleOrDefaultAsync(match);
        }

        public ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().Where(match).ToList();
        }

        public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().Where(match).ToListAsync();
        }

        public TObject Add(TObject t)
        {
            _context.Set<TObject>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public async Task<TObject> AddAsync(TObject t)
        {
            _context.Set<TObject>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public TObject Update(TObject updated, long key)
        {
            if (updated == null)
            {
                return null;
            }

            TObject existing = _context.Set<TObject>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public async Task<TObject> UpdateAsync(TObject updated, long key)
        {
            if (updated == null)
            {
                return null;
            }

            TObject existing = await _context.Set<TObject>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public void Delete(TObject t)
        {
            _context.Set<TObject>().Remove(t);
            _context.SaveChanges();
        }

        public async Task<long> DeleteAsync(TObject t)
        {
            _context.Set<TObject>().Remove(t);
            return await _context.SaveChangesAsync();
        }

        public long Count()
        {
            return _context.Set<TObject>().Count();
        }

        public bool Any(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().Any(match);
        }

        public async Task<long> CountAsync()
        {
            return await _context.Set<TObject>().CountAsync();
        }
    }
}

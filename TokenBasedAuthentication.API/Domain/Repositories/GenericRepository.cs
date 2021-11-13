using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TokenContext _context;
        private DbSet<T> _entry = null;

        public GenericRepository(TokenContext context)
        {
            _context = context;
            _entry = context.Set<T>();
        }

        public async Task Add(T entry)
        {
            await _entry.AddAsync(entry);
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await _entry.CountAsync(predicate);
        }

        public async Task Delete(int id)
        {
            T exist = await this.GetById(id);

            if (exist != null)
                _entry.Remove(exist);
        }

        public async Task<T> GetById(int id)
        {
            return await _entry.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _entry.Where(predicate).ToListAsync();
        }

        public void Update(T entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
        }
    }
}
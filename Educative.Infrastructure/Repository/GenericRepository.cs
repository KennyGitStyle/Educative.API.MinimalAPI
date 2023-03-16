using Educative.Infrastructure.Data.Context;
using Educative.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EducativeContext _context;

        public GenericRepository(EducativeContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIDAsync(string? ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new ArgumentException("ID parameter cannot be null or empty.", nameof(ID));
            }

            var result = await _context.Set<T>().FindAsync(ID);
            if (result == null)
            {
                throw new ArgumentException("No record found for the given ID.", nameof(ID));
            }

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(string ID)
        {
            var checkRecord = await _context.Set<T>().FindAsync(ID);
            if(checkRecord != null)
            {
                _context.Remove(checkRecord);
                return await _context.SaveChangesAsync() == 1;
            }
            return await _context.SaveChangesAsync() == 0;
        }
    }
}
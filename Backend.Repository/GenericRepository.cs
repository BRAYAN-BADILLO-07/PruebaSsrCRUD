using Backend.Domain.Interfaces;
using Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync( T entity )
        {
             await _context.Set<T>().AddAsync(entity); 
        }

        public void Delete( T entity )
        {
             _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync( short code )
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public void Update( T entity )
        {
            _context.Set<T>().Update(entity);
        }


    }
}

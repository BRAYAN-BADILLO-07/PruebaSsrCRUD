using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync( T entity );
        void Update( T entity );
        void Delete( T entity );
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync( short code );
    }
}

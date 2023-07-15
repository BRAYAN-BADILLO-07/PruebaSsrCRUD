using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Seller> Sellers { get; }
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Seller>> GetAllAsy();
    }
}

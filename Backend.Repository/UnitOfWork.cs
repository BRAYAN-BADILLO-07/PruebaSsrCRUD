using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IGenericRepository<City> _cityRepository;
        private  IGenericRepository<Seller> _sellerRepository;


        public UnitOfWork(ApplicationDbContext context )
        {
            _context = context;
        }

        public IGenericRepository<City> Cities => _cityRepository ??  new GenericRepository<City>(_context);

        public IGenericRepository<Seller> Sellers => _sellerRepository ?? new GenericRepository<Seller>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return  await _context.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose(bool disponsing) 
        {
            if(disponsing )
            {
                _context.Dispose();
            }
        }

        public async Task<IEnumerable<Seller>> GetAllAsy()
        {
            return await _context.Sellers.Include(c => c.City).ToListAsync();
        }
    }
}

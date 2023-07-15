using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class SellerRepository: GenericRepository<Seller>, ISellerRepository 
    {
        private readonly ApplicationDbContext _context;
        public SellerRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }

    }
}

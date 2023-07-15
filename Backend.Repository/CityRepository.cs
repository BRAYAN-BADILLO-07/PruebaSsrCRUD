using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Backend.Repository.Models;

namespace Backend.Repository
{
    public class CityRepository: GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context) { }

    }
}

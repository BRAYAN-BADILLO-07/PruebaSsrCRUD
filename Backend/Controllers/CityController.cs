using AutoMapper;
using Backend.Domain.DTOs;
using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityController( IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Cities 
        /// </summary>
        /// <returns> List of Cities </returns>
        [HttpGet(nameof(GetCities))]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            IEnumerable<City> listCities = await _unitOfWork.Cities.GetAllAsync();
            List<CityDTO> listCitiesDTO = _mapper.Map<List<CityDTO>>(listCities);

            if(listCitiesDTO.Count > 0)
                return Ok(listCitiesDTO);

            return NotFound();
        }  

        /// <summary>
        /// Search and Get City For Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Details of the City</returns>
        [HttpGet("{code}")]
        public async Task<ActionResult<CityDTO>> GetCity( short code )
        {
            City city = await _unitOfWork.Cities.GetByIdAsync(code);
            CityDTO cityDTO = _mapper.Map<CityDTO>(city);

            if ( cityDTO == null )
                return NotFound();

            return Ok(cityDTO);
        }

        /// <summary>
        /// Create a City
        /// </summary>
        /// <param name="cityDTO"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpPost]
        public async Task<ActionResult<CityDTO>> CreateCity( CityDTO cityDTO )
        {
            City city = _mapper.Map<City>(cityDTO);
            await _unitOfWork.Cities.CreateAsync(city);
            int result = await _unitOfWork.SaveChangesAsync();

            if(result != 0)
                return Ok(cityDTO);

            return ValidationProblem(statusCode: StatusCodes.Status500InternalServerError);
        }


        /// <summary>
        /// Update City For Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cityDTO"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateCity( short code, CityDTO cityDTO )
        {
            if ( code != cityDTO.Code )
            {
                return NotFound();
            }
            City city = _mapper.Map<City>(cityDTO);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Delete City for Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteCity( short code )
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(code);
            if ( city == null )
            {
                return NotFound();
            }
            _unitOfWork.Cities.Delete(city);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

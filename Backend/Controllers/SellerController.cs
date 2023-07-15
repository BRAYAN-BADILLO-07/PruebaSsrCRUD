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
    public class SellerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SellerController( IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Get All Sellers 
        /// </summary>
        /// <returns>List of Sellers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerDTO>>> GetSellers()
        {
            IEnumerable<Seller> sellers = await _unitOfWork.GetAllAsy();
            List<SellerDTO> sellersDTO = _mapper.Map<List<SellerDTO>>(sellers);

            return Ok(sellersDTO);
        }

        /// <summary>
        /// Search and Get Seller For Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Details of Seller</returns>
        [HttpGet("{code}")]
        public async Task<ActionResult<SellerDTO>> GetSeller( short code )
        {
            var Seller = await _unitOfWork.Sellers.GetByIdAsync(code);
            SellerDTO SellersDTO = _mapper.Map<SellerDTO>(Seller);

            if ( SellersDTO == null )
                return NotFound();

            return Ok(SellersDTO);
        }

        /// <summary>
        /// Create a Seller 
        /// </summary>
        /// <param name="SellerDTO"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpPost]
        public async Task<ActionResult<SellerDTO>> CreateSeller( SellerDTO SellerDTO )
        {
            Seller Seller = _mapper.Map<Seller>(SellerDTO);
            await _unitOfWork.Sellers.CreateAsync(Seller);
            int result = await _unitOfWork.SaveChangesAsync();

            if ( result != 0 )
                return CreatedAtAction("GetSeller", new { code = Seller.Code }, SellerDTO);
            else
                return ValidationProblem(statusCode: StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Update Seller For Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="SellerDTO"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateSeller( short code, SellerDTO SellerDTO )
        {
            if ( code != SellerDTO.Code )
                return BadRequest();

            Seller Seller = _mapper.Map<Seller>(SellerDTO);
            _unitOfWork.Sellers.Update(Seller);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Delete Seller For Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Http Code 200 or 500</returns>
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteSeller( short code )
        {
            var Seller = await _unitOfWork.Sellers.GetByIdAsync(code);

            if ( Seller == null )
                return NotFound();

            _unitOfWork.Sellers.Delete(Seller);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }

}

using API.DTOs;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MVC.Models;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitofWork;
        //private readonly APIResponse<List<OrderItem>> _response;
		//private readonly APIResponse<List<Address>> _response;
		private readonly APIResponse<object> _response;


		public AddressController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAddresses()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Errors = new List<string> { "User not authorized" };
                return Unauthorized(_response);
            }

            var Addresses = await _unitofWork.Addresses.GetAllAsync(o => o.UserId == userId);
            _response.Data = Addresses;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _unitofWork.Addresses.GetAsync(a => a.Id == id);
            if (address == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Address Not Found" };
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = address;


            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateAddress([FromBody] AddressDto addressDto)
        {
            try
            {
                if (addressDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        _response.StatusCode = HttpStatusCode.Unauthorized;
                        _response.Errors = new List<string> { "User not authorized" };
                        return Unauthorized(_response);
                    }
                    var entity = new Address
                    {
                        Street = addressDto.Street,
                        City = addressDto.City,
                        Governorate = addressDto.Governorate,
                        PostalCode = addressDto.PostalCode,
                        UserId = userId
                    };

                    await _unitofWork.Addresses.AddAsync(entity);
                    await _unitofWork.SaveAsync();
                    _response.Data = entity;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateAddress(int id, [FromBody] AddressDto addressdto)
        {
            if (addressdto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var address = await _unitofWork.Addresses.GetAsync(a => a.Id == id);
                    address.Street = addressdto.Street;
                    address.City = addressdto.City;
                    address.Governorate = addressdto.Governorate;
                    address.PostalCode = addressdto.PostalCode;

                    await _unitofWork.SaveAsync();
                    _response.Data = addressdto;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                catch (Exception ex)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }

            _response.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteAddress(int id)
        {
            try
            {
                var address = await _unitofWork.Addresses.GetAsync(c => c.Id == id);
                if (address == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Errors = new List<string> { "Address Not Found" };
                    return NotFound(_response);
                }

                await _unitofWork.Addresses.RemoveAsync(address);
                await _unitofWork.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { ex.Message };
                return NotFound(_response);
            }
        }
    }
}

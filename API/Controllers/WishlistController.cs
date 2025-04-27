using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishlistController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly APIResponse<Wishlist> _response;


		public WishlistController(IUnitOfWork unitOfWork, APIResponse<Wishlist> response)
		{
			_unitOfWork = unitOfWork;
			_response = response;
		}

		[HttpGet]
		public async Task<ActionResult<APIResponse<Wishlist>>> GetWishlist()
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.Errors = new List<string> { "User ID not found" };
					return BadRequest(_response);
				}

				var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserId(userId);
				_response.StatusCode = HttpStatusCode.OK;
				_response.Data = wishlist;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(500, _response);
			}
		}

		[HttpPost]
		public async Task<ActionResult<APIResponse<bool>>> AddToWishlist(int productId)
		{
			var response = new APIResponse<bool>(
			HttpStatusCode.BadRequest,  // Status code
			"Something went wrong",  // Message
			false  // Data (bool value)
			);

			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					response.Errors = new List<string> { "User ID not found" };
					return BadRequest(response);
				}

				bool added = await _unitOfWork.Wishlists.AddProductToWishlist(productId, userId);
				response.Data = added;
				response.StatusCode = HttpStatusCode.OK;

				return Ok(response);
			}
			catch (Exception ex)
			{
				response.StatusCode = HttpStatusCode.BadRequest;
				response.Errors = new List<string> { ex.Message };
				return BadRequest(response);
			}
		}

		[HttpDelete]
		public async Task<ActionResult<APIResponse<bool>>> RemoveWishlistProduct(int productId)
		{


				var response = new APIResponse<bool>(
				HttpStatusCode.BadRequest,  // Status code
				"Something went wrong",  // Message
				false  // Data (bool value)
				);

			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					response.Errors = new List<string> { "User ID not found" };
					return BadRequest(response);
				}

				bool removed = await _unitOfWork.Wishlists.RemoveFromWishlist(productId, userId);
				response.Data = removed;
				response.StatusCode = HttpStatusCode.OK;

				return Ok(response);
			}
			catch (Exception ex)
			{
				response.StatusCode = HttpStatusCode.BadRequest;
				response.Errors = new List<string> { ex.Message };
				return BadRequest(response);
			}
		}
	}
}

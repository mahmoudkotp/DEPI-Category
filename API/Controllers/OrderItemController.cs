using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderItemController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly APIResponse _response;

		public OrderItemController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_response = new APIResponse();
		}

		[HttpGet("order/{orderId}")]
		public async Task<ActionResult<APIResponse>> GetOrderItemsByOrderId(int orderId)
		{
			var orderItems = await _unitOfWork.OrderItems.GetOrderItemsByOrderIdAsync(orderId);
			if (orderItems == null || !orderItems.Any())
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.Errors = new List<string> { "No order items found for this order." };
				return NotFound(_response);
			}

			_response.StatusCode = HttpStatusCode.OK;
			_response.Data = orderItems;
			return Ok(_response);
		}
	}
}

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
	public class OrderController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly APIResponse<List<Order>> _response;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_response = new APIResponse<List<Order>>(
			  HttpStatusCode.OK,    // status
			  "Data loaded successfully",  // message
			  new List<Order>()  // empty list for default response
		  );
		}

		[HttpGet]
		public async Task<ActionResult<APIResponse<List<Order>>>> GetUserOrders()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				_response.StatusCode = HttpStatusCode.Unauthorized;
				_response.Errors = new List<string> { "User not authorized" };
				return Unauthorized(_response);
			}

			//var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
			var orders = (await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId)).ToList();
			_response.StatusCode = HttpStatusCode.OK;
			_response.Data = orders;
			return Ok(_response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<APIResponse<List<Order>>>> GetOrder(int id)
		{
			var order = await _unitOfWork.Orders.GetAsync(
				o => o.Id == id,
				includeProperties: "OrderItems,Address");
			if (order == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.Errors = new List<string> { "Order not found" };
				return NotFound(_response);
			}

			_response.Data = new List<Order> { order }; 
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}

	}
}

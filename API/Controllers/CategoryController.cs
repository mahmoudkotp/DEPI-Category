using Azure;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var categories = _unitOfWork.CategoryRepository.GetAll();

			if (categories == null || !categories.Any())
			{
				var response = new APIResponse<List<Category>>(
					HttpStatusCode.NotFound,
					"No categories found",
					null
				);
				return NotFound(response);
			}

			var successResponse = new APIResponse<List<Category>>(
				HttpStatusCode.OK,
				"Categories retrieved successfully",
				categories.ToList()
			);
			return Ok(successResponse);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var category = _unitOfWork.CategoryRepository.GetById(id);
			if (category == null)
			{
				var response = new APIResponse<Category>(
					HttpStatusCode.NotFound,
					"Category not found",
					null
				);
				return NotFound(response);
			}

			var successResponse = new APIResponse<Category>(
				HttpStatusCode.OK,
				"Category retrieved successfully",
				category
			);
			return Ok(successResponse);
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				var errorResponse = new APIResponse<object>(
					HttpStatusCode.BadRequest,
					"Validation failed",
					ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				);
				return BadRequest(errorResponse);
			}

			_unitOfWork.CategoryRepository.Add(category);

			var response = new APIResponse<Category>(
				HttpStatusCode.Created,
				"Category created successfully",
				category
			);

			return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Category category)
		{
			if (id != category.Id)
			{
				var errorResponse = new APIResponse<string>(
					HttpStatusCode.BadRequest,
					"ID mismatch",
					null
				);
				return BadRequest(errorResponse);
			}

			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
			{
				var notFoundResponse = new APIResponse<string>(
					HttpStatusCode.NotFound,
					"Category not found",
					null
				);
				return NotFound(notFoundResponse);
			}

			existing.Name = category.Name;
			_unitOfWork.CategoryRepository.Update(existing);

			var successResponse = new APIResponse<Category>(
				HttpStatusCode.OK,
				"Category updated successfully",
				existing
			);

			return Ok(successResponse);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
			{
				var notFoundResponse = new APIResponse<string>(
					HttpStatusCode.NotFound,
					"Category not found",
					null,
					new List<string> { "The category with the provided ID does not exist." }
				);
				return NotFound(notFoundResponse);
			}

			_unitOfWork.CategoryRepository.Delete(existing);
			await _unitOfWork.SaveAsync();

			var successResponse = new APIResponse<string>(
				HttpStatusCode.OK,
				"Category successfully deleted",
				null
			);

			return Ok(successResponse);
		}

		[HttpGet("search/{name}")]
		public IActionResult SearchByName(string name)
		{
			var categories = _unitOfWork.CategoryRepository.SearchByName(name);
			if (!categories.Any())
				return NotFound();

			return Ok(categories);
		}

		[HttpGet("{id}/products")]
		public IActionResult GetCategoryWithProducts(int id)
		{
			var category = _unitOfWork.CategoryRepository.GetCategoryWithProducts(id);
			if (category == null)
				return NotFound();

			return Ok(category);
		}
	}
}

using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{		
		private readonly IUnitOfWork _unitOfWork;

		// Constructor Dependency Injection
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult GetAll()
		{			
			var categories = _unitOfWork.CategoryRepository.GetAll();
			return Ok(categories);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var category = _unitOfWork.CategoryRepository.GetById(id);
			if (category == null)
				return NotFound();
		
			return Ok(category);
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_unitOfWork.CategoryRepository.Add(category);
			return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Category category)
		{
			if (id != category.Id)
				return BadRequest("ID mismatch");

			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
				return NotFound();

			existing.Name = category.Name;
			_unitOfWork.CategoryRepository.Update(existing);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
				return NotFound();

			_unitOfWork.CategoryRepository.Delete(existing);
			return NoContent();
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

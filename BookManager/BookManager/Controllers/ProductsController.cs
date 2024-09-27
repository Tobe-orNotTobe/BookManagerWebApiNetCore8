using BookManager.Models;
using BookManager.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IBookRepository _bookRepo;

		public ProductController(IBookRepository bookRepo)
		{
			_bookRepo = bookRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBooks()
		{
			try
			{
				return Ok(await _bookRepo.GetAllBookAsync());
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpGet("id")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var book = await _bookRepo.GetBookAsync(id);
			return book == null ? NotFound() : Ok(book);
		}

		[HttpPost]
		public async Task<IActionResult> AddnewBook(BookModel model)
		{
			try
			{
				var newBookId = await _bookRepo.AddBookAsync(model);
				var book = await _bookRepo.GetBookAsync(newBookId);
				return book == null ? NotFound() : Ok(book);
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, BookModel model)
		{
			if(id != model.Id)
			{
				return NotFound();
			}
			await _bookRepo.UpdateBookAsync(id, model);
			return Ok();
		}

		[HttpDelete("id")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			await _bookRepo.DeleteBookAsync(id); return Ok();
		}
	}
}

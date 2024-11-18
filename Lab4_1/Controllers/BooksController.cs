using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab4_1.Models;
using Lab4_1.ModelsUpdate;
using AutoMapper;
using Lab4_1.Injection;
using Lab4_1.ModelsView;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET all books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);
            return Ok(bookViewModels);
        }

        // GET book by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            var bookViewModel = _mapper.Map<BookViewModel>(book);
            return Ok(bookViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookCreateUpdateViewModel model)
        {
            var book = _mapper.Map<Book>(model);

            await _bookService.Books.AddAsync(book);
            await _bookService.SaveChangesAsync();

            var bookViewModel = _mapper.Map<BookViewModel>(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, bookViewModel);
        }

        private decimal? CalculateFinalPrice(decimal? price, decimal? discount)
        {
            if (price.HasValue && discount.HasValue)
            {
                return price - (price * discount / 100);
            }
            return price;
        }

        // PATCH partial update
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPartially(int id, [FromBody] BookUpdate bookUpdate)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, bookUpdate);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Book not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE book
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Book not found");
            }
        }
    }
}

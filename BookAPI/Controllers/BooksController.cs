using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Repositories;
using BookAPI.Models;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            return await _bookRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookRepository.create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook }, newBook);
        }

        [HttpPut]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book) 
        {
            if(id != book.id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DelBooks(int id)
        {
            var bookToDelte = await _bookRepository.Get(id);
            if (bookToDelte == null)
                return NotFound();

            await _bookRepository.Delete(bookToDelte.id);
            return NoContent();
        }

    }
}

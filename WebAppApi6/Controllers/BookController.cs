using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CQRS.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppApi6.Models;
using WebAppApi6.Services;

namespace WebAppApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ApiBaseController
    {
        //[Authorize(Policy = "IsActivityHost")]
        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAsync()
        {
            //var bookService = new InMemoryBookService();

            //return Ok(bookService.GetBooksAsync().Result);

            var re = await Mediator.Send(new GetBooksQuery());

            return Ok(re);
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetBookById")]
        //[Route("GetBookById")]
        public string GetBookById(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<string>> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Book
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryTestApp.Models;
using LibraryTestApp.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BookLibraryApp.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        // create/edit
        [HttpPost]
        public JsonResult CreateEdit([FromBody]Book? book)
        {
            if (book != null)
            {
                // (book.Id == 0)
                _context.Books.Add(book);
                /*else
                {
                    var bookInDb = _context.Books.Find(book);
                    if (bookInDb != null || book.Title == "" || book.Author == "" || book.Genre == "")
                        return new JsonResult(BadRequest());

                    bookInDb = book;
                }*/

                _context.SaveChanges();

                return new JsonResult(Ok(book));
            }

            return new JsonResult(BadRequest("Book ot DB is null"));
        }

        // get
        [HttpGet("get-book")]
        public JsonResult Get(int id)
        {
            if (_context != null)
            {
                var result = _context.Books.Find(id);

                if (result == null)
                    return new JsonResult(NotFound());

                return new JsonResult(Ok(result));
            }

            return new JsonResult(BadRequest());
        }

        // delete
        [HttpDelete("remove-book")]
        public JsonResult Delete(int id)
        {
            var result = _context.Books.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Books.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }

        // get all
        [HttpGet("get-all")]
        public JsonResult GetAll()
        {
            if (_context != null)
            {
                var result = _context.Books.ToList();

                return new JsonResult(Ok(result));
            }
            return new JsonResult(BadRequest());
        }
    }
}

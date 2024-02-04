using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryTestApp.Models;
using LibraryTestApp.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace LibraryTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookUserController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        private BookUserController(LibraryDbContext context)
        {
            _context = context;
        }

        // create/edit
        [HttpPost("borrow-book")]
        public JsonResult CreateEdit(BookUser bookUser)
        {
            if (bookUser.Id == 0)
                _context.BooksUsers.Add(bookUser);
            else
            {
                var bookUserInDb = _context.BooksUsers.Find(bookUser);

                if (bookUserInDb != null)
                    return new JsonResult(BadRequest());

                bookUserInDb = bookUser;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(bookUser));
        }

        // delete
        [HttpDelete("return-book")]
        public JsonResult Delete(int id)
        {
            var result = _context.BooksUsers.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.BooksUsers.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }
    }
}

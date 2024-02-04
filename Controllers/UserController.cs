using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryTestApp.Models;
using LibraryTestApp.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BookLibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public UserController(LibraryDbContext context) {
            _context = context;
        }

        public bool CheckForDB()
        {
            return _context.Database.EnsureCreated();
        }

        // create/edit
        [HttpPost("register-user")]
        public JsonResult CreateEdit(User user)
        {
            if (CheckForDB())
            {
                if (user.Id == 0)
                    _context.Users.Add(user);
                else
                {
                    var userInDb = _context.Users.Find(user);

                    if (userInDb != null || user.Name == "" || user.Email == "")
                        return new JsonResult(BadRequest());

                    userInDb = user;
                }

                _context.SaveChanges();

                return new JsonResult(Ok(user));
            }

            return new JsonResult(NotFound());
        }

        // get
        [HttpGet("get-user")]
        public JsonResult Get(int id) {
            var result = _context.Users.Find(id);
            
            if(result == null)
                return new JsonResult(NotFound());

            return new JsonResult(result);
        }

        // delete
        [HttpDelete("remove-user")]
        public JsonResult Delete(int id) {
            var result = _context.Users.Find(id);

            if(result == null)
                return new JsonResult(NotFound());

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }

        // get all
        [HttpGet("get-all")]
        public JsonResult GetAll() {
            var result = _context.Users.ToList();

            return new JsonResult(Ok(result));
        }

    }
}

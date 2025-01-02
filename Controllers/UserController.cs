using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_API.Data;
using ToDo_API.Models;

namespace ToDo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _UserRepository;
        public UserController(UserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _UserRepository.GetAllUser();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = _UserRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(UserModel User)
        {
            if (User == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.AddUsers(User);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }



        [HttpPut]
        public IActionResult UpdateUser(UserModel User)
        {
            if (User == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.UpdateUser(User);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }



        [HttpDelete]
        public IActionResult DeleteUser(int UserID)
        {

            bool isinserted = _UserRepository.DeleteUser(UserID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }


    }
}

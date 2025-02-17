using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_API.Data;
using ToDo_API.Models;

namespace ToDo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Constructor Dependency Injection
        private readonly UserRepository _UserRepository;
        public UserController(UserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }
        #endregion

        #region GetAllUser
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var user = _UserRepository.GetAllUser();
            return Ok(user);
        }
        #endregion

        #region GetUserByID
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
        #endregion

        #region AddUser
        [HttpPost]
        [Route("register")]
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
        #endregion

        #region Login
        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var user = _UserRepository.Login(request);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        #endregion

        #region UpdateUser
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
        #endregion

        #region UserActive
        [HttpPut("active")]
        public IActionResult UserActive(int UserID)
        {
            if (UserID == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.UserActive(UserID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        #endregion

        #region UserInActive
        [HttpPut("inactive")]
        public IActionResult UserInActive(int UserID)
        {
            if (UserID == null)
            {
                return BadRequest();
            }
            bool isinserted = _UserRepository.UserInActive(UserID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        #endregion

        #region DeleteUser
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
        #endregion

    }
}

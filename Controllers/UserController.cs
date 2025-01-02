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
    }
}

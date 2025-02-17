using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_API.Data;
using ToDo_API.Models;

namespace ToDo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _CategoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            this._CategoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var category = _CategoryRepository.GetAllCategory();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryByID(int id)
        {
            var category = _CategoryRepository.GetCategoryByID(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet("jwtauth")]
        public IActionResult GetToken() {
            var userId = HttpContext.Items["UserId"];

            if (userId == null)
            {
                Console.WriteLine("Unauthorized: Token validation failed or user not attached to context.");
                return Unauthorized("Invalid or expired token.");
            }

            var donor = _CategoryRepository.GetCategoryByUserID((int)userId);
            if (donor == null)
            {
                return NotFound("Donor not found.");
            }

            return Ok(donor);
        }

        [HttpGet("by-user/{userid}")]
        /*[Route("userid")]*/
        public IActionResult GetTaskByUserID(int userid)
        {
            var category = _CategoryRepository.GetCategoryByUserID(userid);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet("dd-by-user/{userid}")]
        /*[Route("userid")]*/
        public IActionResult GetCatDDByUserID(int userid)
        {
            var category = _CategoryRepository.GetCategoryDropDownByUser(userid);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPost]
        public IActionResult AddTask(CategoryModel Category)
        {
            if (Category == null)
            {
                return BadRequest();
            }
            bool isinserted = _CategoryRepository.AddCategory(Category);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryModel Category)
        {
            if (Category == null)
            {
                return BadRequest();
            }
            bool isinserted = _CategoryRepository.UpdateCategory(Category);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int CategoryID)
        {

            bool isinserted = _CategoryRepository.DeleteCategory(CategoryID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

    }
}

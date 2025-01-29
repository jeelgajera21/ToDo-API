using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_API.Data;
using ToDo_API.Models;

namespace ToDo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly ReminderRepository _ReminderRepository;
        public ReminderController(ReminderRepository reminderRepository)
        {
            this._ReminderRepository = reminderRepository;
        }

        [HttpGet]
        public IActionResult GetAllReminder()
        {
            var reminder = _ReminderRepository.GetAllReminder();
            return Ok(reminder);
        }

        [HttpGet("{id}")]
        public IActionResult GetReminderByID(int id)
        {
            var reminder = _ReminderRepository.GetReminderByID(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return Ok(reminder);
        }

        [HttpGet("by-task/{taskid}")]
        /*[Route("userid")]*/

        public IActionResult GetReminderByTaskID(int taskid)
        {
            var reminder = _ReminderRepository.GetReminderByTaskID(taskid);
            if (reminder == null)
            {
                return NotFound();
            }
            return Ok(reminder);
        }
        [HttpGet("by-user/{userid}")]
        /*[Route("userid")]*/

        public IActionResult GetReminderByUserID(int userid)
        {
            var reminder = _ReminderRepository.GetReminderByUserID(userid);
            if (reminder == null)
            {
                return NotFound();
            }
            return Ok(reminder);
        }


        [HttpPost]
        public IActionResult AddReminder(ReminderModel Reminder)
        {
            if (Reminder == null)
            {
                return BadRequest();
            }
            bool isinserted = _ReminderRepository.AddReminder(Reminder);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult UpdateReminder(ReminderModel Reminder)
        {
            if (Reminder == null)
            {
                return BadRequest();
            }
            bool isinserted = _ReminderRepository.UpdateReminder(Reminder);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteReminder(int ReminderID)
        {

            bool isinserted = _ReminderRepository.DeleteReminder(ReminderID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut("sent")]
        public IActionResult ReminderSent(int ReminderID)
        {
            if (ReminderID == null)
            {
                return BadRequest();
            }
            bool isinserted = _ReminderRepository.ReminderSent(ReminderID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }


    }
}

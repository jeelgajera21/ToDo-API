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
        #region Constructor Dependency Injection
        private readonly ReminderRepository _ReminderRepository;
        public ReminderController(ReminderRepository reminderRepository)
        {
            this._ReminderRepository = reminderRepository;
        }
        #endregion

        #region GetAllReminder
        [HttpGet]
        public IActionResult GetAllReminder()
        {
            var reminder = _ReminderRepository.GetAllReminder();
            return Ok(reminder);
        }
        #endregion

        #region GetReminderByID
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
        #endregion

        #region GetReminderByTaskID
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
        #endregion

        #region GetReminderByUserID
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
        #endregion

        #region AddReminder
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
        #endregion

        #region UpdateReminder
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
        #endregion

        #region DeleteReminder
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
        #endregion

        #region ReminderSent
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
        #endregion


    }
}

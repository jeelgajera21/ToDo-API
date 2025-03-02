﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_API.Data;
using ToDo_API.Models;

namespace ToDo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        #region Constructor Dependency Injection
        private readonly TaskRepository _TaskRepository;
        public TaskController(TaskRepository taskRepository)
        {
            this._TaskRepository = taskRepository;
        }
        #endregion

        #region GetAllTask
        [HttpGet]
        public IActionResult GetAllTask()
        {
            var user = _TaskRepository.GetAllTasks();
            return Ok(user);
        }
        #endregion

        #region GetTaskByID
        [HttpGet("{id}")]
        public IActionResult GetTaskByID(int id)
        {
            var user = _TaskRepository.GetTaskByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        #endregion

        #region GetTaskByUserID
        [HttpGet("by-user/{userid}")]
        /*[Route("userid")]*/
       
        public IActionResult GetTaskByUserID(int userid)
        {
            var user = _TaskRepository.GetTaskByUserID(userid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        #endregion

        #region AddTask
        [HttpPost]
        public IActionResult AddTask(TaskModel Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }
            bool isinserted = _TaskRepository.AddTask(Task);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        #endregion

        #region GetTaskByCategoryID
        [HttpGet("dd-by-user/{userid}")]
        /*[Route("userid")]*/
        public IActionResult GetTaskDDByUserID(int userid)
        {
            var task = _TaskRepository.GetTaskDropDownByUser(userid);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        #endregion

        #region UpdateTask
        [HttpPut]
        public IActionResult UpdateTask(TaskModel Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }
            bool isinserted = _TaskRepository.UpdateTask(Task);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        #endregion

        #region DeleteTask
        [HttpDelete]
        public IActionResult DeleteTask(int TaskID)
        {

            bool isinserted = _TaskRepository.DeleteTask(TaskID);
            if (isinserted)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        #endregion

    }
}

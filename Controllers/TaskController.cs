using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebOopPrac_Api.Repository;
using static WebOopPrac_Api.Models.ServiceResponse;
using static WebOopPrac_Api.Models.TodoModel;

namespace WebOopPrac_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : ControllerBase
    {
        private readonly IAccount _acc;

        public TaskController(IAccount acc)
        {
            _acc = acc;
        }

        [HttpGet]
        [Route("InsertData/{Title}/{Description}/{IsCompleted}")]
        public async Task<IActionResult> CredInsert(string Title, string Description, bool IsCompleted)
        {
            try
            {
                ServiceResponseModel<IEnumerable<dynamic>> Response = await _acc.CredInsert(Title, Description, IsCompleted);

                return StatusCode((int)Response.HttpCode, Response);
            }
            catch (Exception ee)
            {
                return StatusCode(500, new { message = "Internal Server Error" });

            }
        }
    }
}

        //[Route("api/[controller]")]
        //[ApiController]
        //public class TaskController : ControllerBase
        //{
        //    //create => post
        //    //read => get
        //    //update => put/patch
        //    //delete => delete

        //    //in memory storage for simplicity
        //    private static readonly List<TodoItem> _todoItems = [];

        //    //get api/tasks
        //    [HttpGet]
        //    public ActionResult<IEnumerable<TodoItem>> Get()
        //    {
        //        return Ok(_todoItems);
        //    }

        //    //get api/tasks/1
        //    [HttpGet("{id}")]
        //    public ActionResult<TodoItem> Get(int id)
        //    {
        //        var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
        //        if (todoItem == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(todoItem);
        //    }
        //    //post api/task
        //    [HttpPost]
        //    public ActionResult Post([FromBody] TodoItem todoItem) 
        //    { 
        //    _todoItems.Add(todoItem);
        //        //return Ok(todoItem);
        //        return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);


        //    }

        //    //put api/task/1
        //    [HttpPut("{id}")]
        //    public ActionResult Put(int id, [FromBody] TodoItem todoItem) 
        //    { 
        //    if (id != todoItem.Id)
        //        {
        //            return BadRequest();
        //        }
        //    var todoItemToUpdate = _todoItems.FirstOrDefault(x => x.Id == id);
        //        if (todoItemToUpdate == null)
        //        {
        //            return NotFound();
        //        }
        //        todoItemToUpdate.Title = todoItem.Title;
        //        todoItemToUpdate.Description = todoItem.Description;
        //        todoItemToUpdate.IsCompleted = todoItem.IsCompleted;

        //        return NoContent();
        //    }

        //    //delete api/task/1
        //    [HttpDelete("{id}")]
        //    public ActionResult Delete(int id)
        //    {
        //        var todoItemToDelete = _todoItems.FirstOrDefault(x =>x.Id == id);
        //        if (todoItemToDelete == null)
        //        {
        //            return NotFound();
        //        }
        //        _todoItems.Remove(todoItemToDelete);
        //        return NoContent();
        //    }
        //}


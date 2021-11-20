using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ToDoWithDapr.Shared;
using ToDoWithDapr.Shared.Models;

namespace ToDoWithDapr.API.Controllers.V1
{
    [ApiController]
    [Route("V1/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public TodoController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAsync()
        {
            return Ok(await GetAllTodoAsync());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Todo todo)
        {
            var todos = await GetAllTodoAsync();
            if (todos == null) todos = new List<Todo>();
            todos.Add(todo);
            await SaveAllTodosAsync(todos);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(string id, Todo todo)
        {
            var guidId = new System.Guid(id);
            var todos = await GetAllTodoAsync();
            if (todos == null) return NotFound();
            var todoToUpdate = todos.FirstOrDefault(t => t.Id == guidId);
            if (todoToUpdate == null) return NotFound();
            todoToUpdate.IsDone = todo.IsDone;
            await SaveAllTodosAsync(todos);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var guidId = new System.Guid(id);
            var todos = await GetAllTodoAsync();
            if (todos == null) return NotFound();
            var todo = todos.FirstOrDefault(t => t.Id == guidId);
            if (todo == null) return NotFound();
            todos.Remove(todo);
            await SaveAllTodosAsync(todos);
            return Ok();
        }

        private async Task<List<Todo>> GetAllTodoAsync() => await _daprClient.GetStateAsync<List<Todo>>(Constants.STORE_NAME, Constants.KEY);
        private async Task SaveAllTodosAsync(List<Todo> todos)
        { 
            await _daprClient.SaveStateAsync<List<Todo>>(Constants.STORE_NAME, Constants.KEY, todos);
        }
    }
}

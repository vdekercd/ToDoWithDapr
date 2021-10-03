using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ToDoWithDapr.API.Models;

namespace ToDoWithDapr.API.Controllers.V1
{
    [ApiController]
    [Route("V1/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        private const string STORE_NAME = "statestore";
        private const string KEY = "todos";

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

        private async Task<List<Todo>> GetAllTodoAsync() => await _daprClient.GetStateAsync<List<Todo>>(STORE_NAME, KEY);
        private async Task SaveAllTodosAsync(List<Todo> todos) => await _daprClient.SaveStateAsync<List<Todo>>(STORE_NAME, KEY, todos);
    }
}

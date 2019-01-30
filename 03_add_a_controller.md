# Add a controller
In the Controllers folder, create a class named TodoController.  

Add this code.  
```
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}
```
The code does...  
1. Defines an API controller class without methods.  
2. Decorates the class with the [ApiController] attribute. This attribute indicates that the controller responds to web API requests. For information about specific behaviors that the attribute enables, see Annotation with ApiController attribute. https://docs.microsoft.com/en-us/aspnet/core/web-api/index?view=aspnetcore-2.2#annotation-with-apicontroller-attribute  
3. Uses DI(Dependency Injection) to inject the database context (TodoContext) into the controller. The database context is used in each of the CRUD methods in the controller.  
4. Adds an item named Item1 to the database if the database is empty. This code is in the constructor, so it runs every time there's a new HTTP request. If you delete all items, the constructor creates Item1 again the next time an API method is called. So it may look like the deletion didn't work when it actually did work.  

### Add GET methods
To provide an API that retrieves to-do items, add the following methods to the TodoController class:  
```
// GET: api/Todo
[HttpGet]
public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
    return await _context.TodoItems.ToListAsync();
}

// GET: api/Todo/5
[HttpGet("{id}")]
public async Task<ActionResult<TodoItem>> GetTodoItem(long id) {
    var todoItem = await _context.TodoItems.FindAsync(id);

    if (todoItem == null) {
        return NotFound();
    }

    return todoItem;
}
```
These methods implement two GET endpoints:  

GET /api/todo  
GET /api/todo/{id}  
Test the app by calling the two endpoints from a browser. For example:  
```
https://localhost:<port>/api/todo  
https://localhost:<port>/api/todo/1  
```
The following HTTP response is produced by the call to GetTodoItems:  
```
[
  {
    "id": 1,
    "name": "Item1",
    "isComplete": false
  }
]
```

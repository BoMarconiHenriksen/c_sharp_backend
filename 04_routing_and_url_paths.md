# Routing and URL paths
The [HttpGet] attribute denotes a method that responds to an HTTP GET request. The URL path for each method is constructed as follows:  

Start with the template string in the controller's Route attribute:  
```
namespace TodoApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _context;
```
1. Replace ```[controller]``` with the name of the controller, which by convention is the controller class name minus the "Controller" suffix. For this sample, the controller class name is TodoController, so the controller name is "todo". ASP.NET Core routing is case insensitive. https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.2  

2. If the [HttpGet] attribute has a route template (for example, [HttpGet("products")]), append that to the path. This sample doesn't use a template. For more information, see Attribute routing with Http[Verb] attributes. https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.2#attribute-routing-with-httpverb-attributes  

In the following GetTodoItem method, "{id}" is a placeholder variable for the unique identifier of the to-do item. When GetTodoItem is invoked, the value of "{id}" in the URL is provided to the method in itsid parameter.  
```
// GET: api/Todo/5
[HttpGet("{id}")]
public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
{
    var todoItem = await _context.TodoItems.FindAsync(id);

    if (todoItem == null)
    {
        return NotFound();
    }

    return todoItem;
}
```
### Return values
The return type of the GetTodoItems and GetTodoItem methods is ActionResult<T> type https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-2.2#actionresultt-type. ASP.NET Core automatically serializes the object to JSON and writes the JSON into the body of the response message. The response code for this return type is 200, assuming there are no unhandled exceptions. Unhandled exceptions are translated into 5xx errors.  

ActionResult return types can represent a wide range of HTTP status codes. For example, GetTodoItem can return two different status values:  

If no item matches the requested ID, the method returns a 404 NotFound error code.  
Otherwise, the method returns 200 with a JSON response body. Returning item results in an HTTP 200 response.  

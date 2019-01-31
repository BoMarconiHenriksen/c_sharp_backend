# XML Kommentarer
Tilføj følgende i .csproj  
```
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
 
</PropertyGroup>
```
Når du slår XML kommentarer til får du debug information for udokumenteret public types og members. Det kommer som en warning message:
```
warning CS1591: Missing XML comment for publicly visible type or member 'TodoController.GetAll()'
```
Det er muligt at fjerne warnings med ```<NoWarn>$(NoWarn);1591</NoWarn>```
```
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```
##### Slå warnings fra for specifikke members ved at omslutte koden med #pragma
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-pragma-warning  

Bruges hvis du har kode som ikke skal vises via API docs.  I nedenstående eksemepl fjernes advarsler for code cs1591 for hele Program klassen. Det er muligt at have flere advarsels kodeer med en komma seperede liste.  
```
namespace TodoApi
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
#pragma warning restore CS1591
}
```
Konfigurer Swagger til at bruge den generede XML fil.  
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<TodoContext>(opt => 
        opt.UseInMemoryDatabase("TodoList"));
    services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info
        {
            Version = "v1",
            Title = "ToDo API",
            Description = "A simple example ASP.NET Core Web API",
            TermsOfService = "None",
            Contact = new Contact
            {
                Name = "Shayne Boyer",
                Email = string.Empty,
                Url = "https://twitter.com/spboyer"
            },
            License = new License
            {
                Name = "Use under LICX",
                Url = "https://example.com/license"
            }
        });

        // Set the comments path for the Swagger JSON and UI.
        // Tilføj de her 3 linjer for at konfigurer Swagger til at bruge den generede XML fil.  
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}
```
Reflection https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection bliver brugt til at builde en XML filnavn, der matcher web API projektet.  
AppContext.BaseDirectory property bruges til at lave en path til XML filen.  

##### /// over metode for at tilføje kommentarer, der bliver vist i swagger
```
/// <summary>
/// Deletes a specific TodoItem.
/// </summary>
/// <param name="id"></param>        
[HttpDelete("{id}")]
public IActionResult Delete(long id)
{
    var todo = _context.TodoItems.Find(id);

    if (todo == null)
    {
        return NotFound();
    }

    _context.TodoItems.Remove(todo);
    _context.SaveChanges();

    return NoContent();
}
```
##### UI bruger json 
```
"delete": {
    "tags": [
        "Todo"
    ],
    "summary": "Deletes a specific TodoItem.",
    "operationId": "ApiTodoByIdDelete",
    "consumes": [],
    "produces": [],
    "parameters": [
        {
            "name": "id",
            "in": "path",
            "description": "",
            "required": true,
            "type": "integer",
            "format": "int64"
        }
    ],
    "responses": {
        "200": {
            "description": "Success"
        }
    }
}
```
##### remarks element
Tilføj et ```<remarks>``` element for at tilføje yderligere information. Det er muligt at tilføje tekst, JSON eller XML.  
```
/// <summary>
/// Creates a TodoItem.
/// </summary>
/// <remarks>
/// Sample request:
///
///     POST /Todo
///     {
///        "id": 1,
///        "name": "Item1",
///        "isComplete": true
///     }
///
/// </remarks>
/// <param name="item"></param>
/// <returns>A newly created TodoItem</returns>
/// <response code="201">Returns the newly created item</response>
/// <response code="400">If the item is null</response>            
[HttpPost]
[ProducesResponseType(201)]
[ProducesResponseType(400)]
public ActionResult<TodoItem> Create(TodoItem item)
{
    _context.TodoItems.Add(item);
    _context.SaveChanges();

    return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
}
```

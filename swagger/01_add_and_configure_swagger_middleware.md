# Add and configure Swagger middleware
Add the Swagger generator to the services collection in the Startup.ConfigureServices (startup.cs) method:  
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<TodoContext>(opt =>
        opt.UseInMemoryDatabase("TodoList"));
    services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    // Only add the below code under the above code.  
    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    });
}
```
##### Import the following namespace to use the Info class:
```
using Swashbuckle.AspNetCore.Swagger;
```
##### In the Startup.Configure method, enable the middleware for serving the generated JSON document and the Swagger UI:
```
public void Configure(IApplicationBuilder app)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    app.UseMvc();
}
```
The preceding UseSwaggerUI method call enables the Static File Middleware. If targeting .NET Framework or .NET Core 1.x, add the Microsoft.AspNetCore.StaticFiles NuGet package to the project.  

Launch the app, and navigate to http://localhost:<port>/swagger/v1/swagger.json. The generated document describing the endpoints appears as shown in Swagger specification (swagger.json).  

The Swagger UI can be found at http://localhost:<port>/swagger. Explore the API via Swagger UI and incorporate it in other programs.  

##### Tip
To serve the Swagger UI at the app's root (http://localhost:<port>/), set the RoutePrefix property to an empty string:  
```
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; /* Add this to serve Swagger UI at app's root http://localhost:<port>/ */
});
```
If using directories with IIS or a reverse proxy, set the Swagger endpoint to a relative path using the ./ prefix. For example, ./swagger/v1/swagger.json. Using /swagger/v1/swagger.json instructs the app to look for the JSON file at the true root of the URL (plus the route prefix, if used). For example, use http://localhost:<port>/<route_prefix>/swagger/v1/swagger.json instead of http://localhost:<port>/<virtual_directory>/<route_prefix>/swagger/v1/swagger.json.  


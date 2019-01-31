# Customize and extend
Swagger gør det muligt at dokumenter objekt modellen, og customize UI til at passe til dit theme.

##### API info and description
AddSwaggerGen metoden tilføjer information som forfatter, licens og beskrivelse(i startup.cs i configureServices metoden):  
```
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
});
```

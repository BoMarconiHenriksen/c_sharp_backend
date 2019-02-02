# Add a model class
En model er et sæt af klasser, der repræsenter de data som appen styrer. Modelen for denne appa er en enkel TodoItem klasse.  

1. Add a folder named Models.  
2. Add a TodoItem class to the Models folder with the following code:  
```
namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
```
Id property funktioner er en unik key i en relationel database.  

Konvention er at model klasser er i en model folder.  

# Add a model class
A model is a set of classes that represent the data that the app manages. The model for this app is a single TodoItem class.  

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
The Id property functions as the unique key in a relational database.  

Model classes can go anywhere in the project, but the Models folder is used by convention.  

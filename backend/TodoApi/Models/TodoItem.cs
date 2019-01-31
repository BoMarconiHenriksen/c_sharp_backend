/* A model is a set of classes that represent the data that the app manages. The model for this app is a single TodoItem class. */
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        /* The Id property functions as the unique key in a relational database. */
        public long Id { get; set; }
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Models
{
    public class TodoItem 
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A Title is required")]
        [MinLength(3, ErrorMessage = "Title must be 3 or more characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A Description is required")]
        [MinLength(3, ErrorMessage = "Title must be 3 or more characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A Description is required")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
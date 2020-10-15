using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using DataModel.Models;

namespace WebApi.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Tasks.Count() == 0)
            {
                context.Tasks.AddRange(
                    new TodoItem 
                    {
                        Title = "task1",
                        Description = "desc1",
                        Date = DateTime.Now
                    },
                    new TodoItem 
                    {
                        Title = "task2",
                        Description = "desc2",
                        Date = DateTime.Now
                    },
                    new TodoItem 
                    {
                        Title = "task3",
                        Description = "desc3",
                        Date = DateTime.Now
                    }
                );
                
                context.SaveChanges();
            }
        }
    }
}
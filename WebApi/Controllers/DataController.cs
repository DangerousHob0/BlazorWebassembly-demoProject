using DataModel.Models;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers {

    [ApiController]
    [Route("/api/tasks")]
    public class DataController : ControllerBase {
        private DataContext context;

        public DataController(DataContext ctx) {
            context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<TodoItem> GetAll() =>  context.Tasks.AsAsyncEnumerable();

        [HttpPost]
        public async Task Save([FromBody]TodoItem item) {
            await context.Tasks.AddAsync(item);
            await context.SaveChangesAsync();
        }


        [HttpGet("{id}")]
        public async Task<TodoItem> GetDetails(long id) =>
                await context.Tasks.FirstAsync(i => i.Id == id);


        [HttpDelete("{id}")]
        public async Task Delete(long id) {
            context.Tasks.Remove(new TodoItem() { Id = id });
            await context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Update([FromBody]TodoItem item) {
            context.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
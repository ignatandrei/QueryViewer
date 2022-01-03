using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastDBToGuiWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppDbContextController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AppDbContextController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public long GetDeps()
        {
            return context.Department.LongCount();
        }
        [HttpPost]
        public Task<long> DepartmentFindNumber(SearchDepartment search)
        {
            return context.DepartmentFindNumber(search);
            
        }
        [HttpGet]
        public async IAsyncEnumerable<Department> DepartmentAsync()
        {
            await foreach(var item in context.DepartmentFindAsync(null))
            {
                await Task.Delay(2000);
                yield return item;
            }

        }
    }
}

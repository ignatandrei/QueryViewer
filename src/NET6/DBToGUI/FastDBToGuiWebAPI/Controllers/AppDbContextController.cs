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
        public long GetDeps()
        {
            return context.Department.LongCount();
        }
        public async Task<long> DepartmentFindNumber(Generated.SearchDepartment search)
        {
            return 34;
        }
    }
}

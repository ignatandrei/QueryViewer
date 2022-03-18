using Generated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkinnyControllersCommon;

namespace ExampleWebAPI.Controllers
{
    //[AutoActions(template = TemplateIndicator.NoArgs_Is_Get_Else_Post, FieldsName = new[] { "*" }, ExcludeFields = new[] { "_logger" })]
    [Route("api/[controller]/[action]")]
    [ApiController]   
    public partial class AppDbContextController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AppDbContextController(ApplicationDbContext context)
        {
            this.context = context;
            
              
            
        }
        //[HttpPost]
        //public string Test()
        //{
        //    SearchDepartment av;
        //    SearchField<eDepartmentColumns> a;
        //    return "dasd";    
               
        //}



}
}

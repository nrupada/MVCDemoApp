using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleDemo.Data;

namespace SampleDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationDbContext.Students.ToListAsync());
        }

        /// <summary>
        ///Students_Read
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> Students_Read([DataSourceRequest] DataSourceRequest request)
        {
            var studentList =  await _applicationDbContext.Students.ToListAsync();
            return Json(studentList.ToDataSourceResult(request));
        }
    }
}
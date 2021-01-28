using Microsoft.AspNetCore.Mvc;
using Wyklad3.Requests;
using Wyklad3.Services;

namespace Wyklad3.Controllers
{

    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase, IStudentsDbService
    {
        private string ConnectionString = "Data Source=db-mssql;Initial Catalog=s19092;Integrated Security=True";


        [HttpGet]
        public IActionResult getEnrollments()
        {
            EnrollStudentRequest e;


            return NotFound();

        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest req)
        {

            return Ok();
        }
    }
}
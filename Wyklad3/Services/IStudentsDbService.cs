using Microsoft.AspNetCore.Mvc;
using Wyklad3.Requests;

namespace Wyklad3.Services
{
    interface IStudentsDbService
    {  
        IActionResult EnrollStudent(EnrollStudentRequest req);
    }
}

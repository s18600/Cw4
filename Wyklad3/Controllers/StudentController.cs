using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Wyklad3.DAL;
using Wyklad3.Models;

namespace Wyklad3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IDbService _dbService;

        public StudentController(IDbService dbService)
        {
            _dbService = dbService;

        }
        [HttpGet]
        public IActionResult GetStudents([FromQuery] string orderBy)
        {
            var orderByToUse = orderBy ?? "IdStudent";
            var orderedEnumerable = _dbService.GetStudents();
            return orderByToUse.ToLower() switch
            {
                "firstname" => Ok(orderedEnumerable.OrderBy(student => student.FirstName)),
                "lastname" => Ok(orderedEnumerable.OrderBy(student => student.LastName)),
                "indexnumber" => Ok(orderedEnumerable.OrderBy(student => student.IndexNumber)),
                _ => Ok(orderedEnumerable.OrderBy(student => student.IdStudent))
            };
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

            student.IndexNumber = $"s{new Random().Next(1,2000)}";
            return Ok(student);

        }
        
        [HttpPut("{id}")]
        public IActionResult putStudent(int id)
        {
            return Ok("Update succeed " + id);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            return Ok("Delete succeed " + id);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult getStudent(string indexNumber)
        {
            var list = new List<Enrollment>();

            string querry = "select * from enrollment e inner join student s on e.idenrollment = s.idenrollment where s.IndexNumber = @index;";
          
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19092;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = querry;

                command.Parameters.AddWithValue("index", indexNumber);

                connection.Open();
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {

                    var en = new Enrollment();
                    en.IdEnrollment = Int32.Parse(dataReader["idenrollment"].ToString());
                    en.Semester =  Int32.Parse(dataReader["Semester"].ToString());
                    en.IdStudy = Int32.Parse(dataReader["IdStudy"].ToString());
                    en.StartDate = (DateTime)dataReader["Birthdate"];
                    
                    list.Add(en);
                }

            }
            return Ok(list);
      
        }

       
    }

}
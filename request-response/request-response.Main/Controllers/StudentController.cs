using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using request_response.Models;
using System.Security.Cryptography.X509Certificates;

namespace request_response.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        private static List<Student> _students = new List<Student>();
        public StudentController()
        {
            if(_students.Count == 0) {
                _students.Add(new Student() { firstName="Nathan", lastName="King"});
                
                
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Create")]
        public async Task<IResult> CreateStudent(Student student)
        {
            _students.Add(student);
            return Results.Created("boolean.com", student);
            

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("All")]
        public async Task<IResult> GetAll()
        {
            return Results.Ok(_students);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("One")]
        public async Task<IResult> GetOne(string first)
        {
            var student = _students.Where(s => s.firstName == first).FirstOrDefault();
            return student != null ? Results.Ok(student) : Results.NotFound();
            
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Update")]
        public async Task<IResult> Update(string firstName, Student newStudent)
        {

            var student = _students.Where(s => s.firstName == firstName).FirstOrDefault();
            if (student == null) return Results.NotFound();
            

            student.firstName = string.IsNullOrEmpty(newStudent.firstName) ? student.firstName : newStudent.firstName;
            student.lastName = string.IsNullOrEmpty (newStudent.lastName) ? student.lastName : newStudent.lastName;


            return Results.Created("boolean.com", student);
            
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Delete")]
        public async Task<IResult> DeleteStudent(string firstName)
        {
            var student = _students.Where(s => s.firstName == firstName).FirstOrDefault();
            int result = _students.RemoveAll(s => s.firstName == firstName);
            return result >= 0 && student!= null ? Results.Ok(student) : Results.NotFound();
        }



    }
}
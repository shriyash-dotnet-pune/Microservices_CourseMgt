using CourseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly List<Course> Courses = new List<Course>
        {
            new() { Id = 1, Title = "Microservices Architecture", Instructor = "Dr. Allen", Credits = 4 },
            new() { Id = 2, Title = "Cloud Computing", Instructor = "Prof. Smith", Credits = 3 },
            new() { Id = 3, Title = "Data Structures", Instructor = "Dr. Lee", Credits = 4 }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Courses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = Courses.FirstOrDefault(c => c.Id == id);
            return Ok(course);
        }

    }
}

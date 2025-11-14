using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.Services;
using System.Threading.Tasks;

namespace StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static readonly List<Enrollment> Enrollments = new List<Enrollment>();
        private readonly StudentClient _studentClient;

        public StudentController(StudentClient studentClient)
        {
            _studentClient = studentClient;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Enrollments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var enrollment = Enrollments.FirstOrDefault(c => c.Id == id);
            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int studentId, string studentName, int courseId)
        {
            var course = await _studentClient.GetCourseByIdAsync(courseId);
            if (course == null) return NotFound("Course is Available");

            var enrollment = new Enrollment();

            enrollment.Id = Enrollments.Count + 1;
            enrollment.StudentId = studentId;
            enrollment.CourseId = course.Id;
            enrollment.StudentName = studentName;
            enrollment.CourseTitle = course.Title;

            Enrollments.Add(enrollment);
            return Ok(enrollment);
        }
    }
}

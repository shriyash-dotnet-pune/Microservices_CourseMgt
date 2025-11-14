namespace StudentService.Services
{
    public class StudentClient
    {
        private readonly HttpClient _httpClient;

        public StudentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"api/course/{id}");
        }

        public class Course
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Instructor { get; set; }
            public int Credits { get; set; }
        }
    }
}

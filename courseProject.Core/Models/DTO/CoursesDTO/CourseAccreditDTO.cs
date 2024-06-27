namespace courseProject.Core.Models.DTO.CoursesDTO
{
    public class CourseAccreditDTO
    {

        public Guid Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? SubAdminFName { get; set; }
        public string? SubAdminLName { get; set; }
        public string? InstructorFName { get; set; }
        public string? InstructorLName { get; set; }
        public int? totalHours { get; set; }



    }
}

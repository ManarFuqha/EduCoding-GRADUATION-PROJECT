

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseProject.Core.Models
{
    public class User 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UserId { get; set; }
        public string userName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        
        public string email {  get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public bool IsVerified { get; set; }
        public string? LName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public string? address { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime dateOfAdded {  get; set; }
        public string? skillDescription { get; set; }      
        public List<Feedback> feedbacks { get; set; }
        public List<Request> requests { get; set; }
        public List<StudentCourse> studentCourses { get; set; }
        public List<Course> courses { get; set; }   
        public List<Consultation> consultations { get; set; }
        public List<InstructorSkills> instructorSkills { get; set; }
        public List<Event> events { get; set; }

    }
}

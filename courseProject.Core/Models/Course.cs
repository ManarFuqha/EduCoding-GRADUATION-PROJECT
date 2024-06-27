using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }

        public string status { get; set; } = "undefined";
        public string category { get; set; }
        [NotMapped] public IFormFile? image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? dateOfAdded { get; set; } = DateTime.Now;
        public DateTime? startDate { get; set;} 
        public DateTime? endDate { get; set;}
        public DateTime? dateOfUpdated { get; set; } 
        public DateTime? Deadline { get; set; }
        public int? limitNumberOfStudnet { get; set; }
        public int? totalHours { get; set; }

        [NotMapped] public bool? isAvailable { get; set; }

        [ForeignKey("User")]
        public Guid InstructorId { get; set; }
        [ForeignKey("User")]
        public Guid subAdminId { get; set; } 
        //[ForeignKey("Request")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public Guid requestId { get; set; }

     
        public User instructor { get; set; }    
        public User subAdmin { get; set; }

        public List<CourseMaterial> Materials { get; set; }

        public List<StudentCourse> studentCourses { get; set; }

        //public Request Request { get; set; }
        public List<Feedback> Feedbacks { get; set; }

    }
}

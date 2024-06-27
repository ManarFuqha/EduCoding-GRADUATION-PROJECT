using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class StudentCourse
    {
        [Key]
        [ForeignKey("User")]
        public Guid StudentId {  get; set; }
        [Key]
        [ForeignKey("Course")]
        public Guid courseId { get; set; }

        public DateTime EnrollDate { get; set; }= DateTime.Now;
        public string status { get; set; } = "waiting";
        [NotMapped] public bool isEnrolled { get; set; }= false;

        public User Student { get; set; }

        public Course Course { get; set; }

    }
}

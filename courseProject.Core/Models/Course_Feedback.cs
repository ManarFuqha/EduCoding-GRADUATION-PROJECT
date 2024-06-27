using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Course_Feedback
    {
        public Guid Id { get; set; }

        public string content { get; set; }

        public DateTime dateOfAdded { get; set; }

        [ForeignKey("User")]
        public Guid StudentId { get; set; }

        [ForeignKey("Course")]
        public Guid courseId { get; set; }

        public User Student { get; set; }
        public Course course { get; set; }

    }
}

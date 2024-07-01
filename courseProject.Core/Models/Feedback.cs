using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Feedback
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string content { get; set; }
            public DateTime dateOfAdded { get; set; } = DateTime.Now;
            public string type { get; set; }

            [Range(0,5)]
            public int? range { get; set; }
        [ForeignKey("Course")]
            [AllowNull]
            public Guid? CourseId { get; set; }

            // public Instructor Instructor { get; set; }
            [ForeignKey("User")]
            public Guid StudentId { get; set; }
        [ForeignKey("User")]
        [AllowNull]
        public Guid? InstructorId { get; set; }

            public User student { get; set; }
            public User instructor { get; set; }
            public Course course { get; set; }
        }
    }


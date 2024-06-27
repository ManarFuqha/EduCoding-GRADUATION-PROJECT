using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class StudentConsultations
    {
        [Key]
        [ForeignKey("User")]
        public Guid StudentId { get; set; }
        [Key]
        [ForeignKey("Consultation")]
        public Guid consultationId { get; set; }

        public DateTime EnrollDate { get; set; }= DateTime.Now;

        public User Student { get; set; }

        public Consultation consultation { get; set; }
    }
}


using System.ComponentModel.DataAnnotations.Schema;


namespace courseProject.Core.Models
{
    public class Consultation
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string type { get; set; }

        //public string status { get; set; } = "off";

        public DateTime date { get; set; } 
        public TimeSpan startTime { get; set; }

        public TimeSpan endTime { get; set; }
         public TimeSpan Duration { get; set; }
        public DateTime dateOfAdded { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public Guid StudentId { get; set; }
        [ForeignKey("User")]
        public Guid InstructorId { get; set; }

      
        public User instructor { get; set; }
     
        public User student { get; set; }
        
        public List<StudentConsultations> studentConsultations { get; set; }
    }
}

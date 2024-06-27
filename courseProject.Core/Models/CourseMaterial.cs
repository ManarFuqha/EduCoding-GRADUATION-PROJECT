using courseProject.core.Models;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;


namespace courseProject.Core.Models
{
    public class CourseMaterial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }
        public string type { get; set; }
      [NotMapped] public List<IFormFile>? pdf {  get; set; }
       //public string? pdfUrl { get; set; }
        public  DateTime dateOfAdded { get; set; }= DateTime.Now;
        public  DateTime? DeadLine { get; set; } 

        //  public DateTime? date { get; set; } = DateOnly.MinValue;
        public string? linkUrl { get; set; }
        public bool isHidden {  get; set; }

        [ForeignKey("User")]
        public Guid InstructorId {  get; set; }

        [ForeignKey("Course")]
        public Guid? courseId {  get; set; }

        [ForeignKey("Consultation")]
        public Guid? consultationId { get; set; }
        public Course Course { get; set; }
        public Consultation consultation { get; set; }
        [ForeignKey("InstructorId")]
        public User instructor { get; set; }
        public List<Student_Task_Submissions> Student_Task_Submissions { get; set; }
        public List<MaterialFiles> MaterialFiles { get; set; }

    }
}

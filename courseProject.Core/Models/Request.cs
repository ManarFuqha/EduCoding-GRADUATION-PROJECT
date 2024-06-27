
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace courseProject.Core.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string name { get; set; } 

        public string satus { get; set; } = "off";

        public string? description { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        //[ForeignKey("User")]
        //[AllowNull]
        //public Guid? SubAdminId { get; set; }

        [ForeignKey("User")]
        [AllowNull]
        public Guid? StudentId { get; set; }

       
   //     public User subAdmin { get; set; }
        public User student { get; set; }

    //    public Event Event { get; set; }
    //    public Course Course { get; set; }
    }
}

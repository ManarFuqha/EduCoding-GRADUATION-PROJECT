using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string? subject { get; set; }
        public string message {  get; set; }
        public DateTime dateOfAdded { get; set; }=DateTime.Now;


    }
}

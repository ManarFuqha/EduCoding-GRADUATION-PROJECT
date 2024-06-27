using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }

        public DateTime dateOfAdded { get; set; } = DateTime.Today;
        
        public DateTime? dateOfEvent {  get; set; }
        [NotMapped] public IFormFile? image { get; set; }
        public string? ImageUrl { get; set; }
        public string category { get; set; }
        public string status { get; set; } = "undefined";

        [ForeignKey("User")]
        public Guid SubAdminId { get; set; }
        //[ForeignKey("Request")]
        //public Guid requestId { get; set; }
        [ForeignKey("SubAdminId")]
        public User subAdmin { get; set; }
        //public Request Request { get; set; }

    }
}

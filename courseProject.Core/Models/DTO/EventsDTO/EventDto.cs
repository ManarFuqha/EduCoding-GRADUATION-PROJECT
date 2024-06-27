using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseProject.Core.Models.DTO.EventsDTO
{
    public class EventDto
    {

        public Guid Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string? ImageUrl { get; set; }
        public string? dateOfEvent { get; set; }


        public Guid SubAdminId { get; set; }

        public string subAdminName { get; set; }
    }
}

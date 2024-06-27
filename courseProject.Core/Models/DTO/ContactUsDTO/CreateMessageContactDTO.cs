using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.ContactUsDTO
{
    public class CreateMessageContactDTO
    {

        public string name { get; set; }
        public string email { get; set; }
        public string? subject { get; set; }
        public string message { get; set; }

    }
}

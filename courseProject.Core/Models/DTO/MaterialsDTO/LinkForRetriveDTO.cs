using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.MaterialsDTO
{
    public class LinkForRetriveDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string linkUrl { get; set; }
        public string type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.InstructorsDTO
{
    public class GetWorkingHourDTO
    {
        public string day { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}

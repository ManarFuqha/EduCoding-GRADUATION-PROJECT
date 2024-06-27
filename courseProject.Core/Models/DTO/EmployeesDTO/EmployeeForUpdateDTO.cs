using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.EmployeesDTO
{
    public class EmployeeForUpdateDTO
    {
      //  public int? Id { get; set; }
        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? email { get; set; }

        public string? phoneNumber { get; set; }

        public string? gender { get; set; }

        public string? address { get; set; }
    
    }
}

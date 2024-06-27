using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.InstructorsDTO
{
    public class WorkingHourDTO
    {
        public DayOfWeek day { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}

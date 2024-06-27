using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace courseProject.Common
{
    public class CommonClass
    {
        
      

       
        public static bool IsValidTimeFormat(string time)
        {
            return Regex.IsMatch(time, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
        }


        public static bool CheckStartAndEndTime(TimeSpan startTime, TimeSpan endTime)
        {


            if (startTime == null || endTime == null)
            {
                return false; // Invalid format or input
            }

            return startTime < endTime; // Ensure start time is before end time
        }

        public static TimeSpan ConvertToTimeSpan (string time)
        {
            
                return TimeSpan.Parse(time);
            
        }

    }
}

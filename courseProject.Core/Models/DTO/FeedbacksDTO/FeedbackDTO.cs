using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.FeedbacksDTO
{
    public class FeedbackDTO
    {
        public string content { get; set; }
        public int? range { get; set; }

    }
}

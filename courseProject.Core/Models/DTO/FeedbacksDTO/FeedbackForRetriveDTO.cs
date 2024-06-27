using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.FeedbacksDTO
{
    public class FeedbackForRetriveDTO
    {
        public string content { get; set; }
        public int? range { get; set; }
        public string name { get; set; }
        public string? imageUrl {  get; set; }
    }
}

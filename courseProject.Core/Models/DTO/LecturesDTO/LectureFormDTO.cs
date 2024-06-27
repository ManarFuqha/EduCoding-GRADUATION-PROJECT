using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.LecturesDTO
{
    public class LectureFormDTO
    {

        public Guid skillId {  get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public DateTime date { get; set; }

    }
}

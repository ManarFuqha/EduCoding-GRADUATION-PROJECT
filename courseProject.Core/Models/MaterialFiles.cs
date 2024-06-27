using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class MaterialFiles
    {
        [Key]
        [ForeignKey("CourseMaterial")]
        public Guid materialId { get; set; }
        [Key]
        public string? pdfUrl { get; set; }

        public CourseMaterial CourseMaterial { get; set; }

      
    }
}

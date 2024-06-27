using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class ApiResponce
    {
      
        public string ErrorMassages { get; set; }// Error messages if any
        public object Result { get; set; }// The result of the API call

    }
}

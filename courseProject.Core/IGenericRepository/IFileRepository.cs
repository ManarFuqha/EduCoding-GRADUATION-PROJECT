using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IFileRepository
    {

        
        public  Task<string> UploadFile1(IFormFile file);
       
        public Task<string> GetFileUrl(string fileName);
        public  Task<List<string>> UploadFiles(List<IFormFile> files);

       
    }
}

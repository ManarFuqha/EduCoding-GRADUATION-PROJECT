using Microsoft.AspNetCore.Http;
using courseProject.Core.IGenericRepository;
using System.IO;
using System.Security.Cryptography;




namespace courseProject.Repository.GenericRepository
{
    public class FileRepository : IFileRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        

        

        public async Task<string> UploadFile1(IFormFile file)
        {
            
            string fileName = "";
            var uploadPath = "";
            try
            {
                // Check if the file is null or empty
                if (file == null || file.Length == 0)
                {
                    return null;
                }


                // Generate a unique file name using the current timestamp
                var extension = Path.GetExtension(file.FileName);
                var name = Path.GetFileName(file.FileName);
                fileName = name + "_"+DateTimeOffset.Now.Ticks.ToString()+"_" +name;
                
                 uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");

                // Create the directory if it does not exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var exactPath = Path.Combine(uploadPath, fileName);

                // Save the file to the specified path
                using (var fileStream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

              
            }
            catch (Exception ex)
            {
                // Return the error message if an exception occurs
                fileName = $"Error: {ex.Message}";
            }
         
           // return file name
            return fileName;
        }



        //Uploads multiple files to the server and returns a list of file names.
        public async Task<List<string>> UploadFiles(List<IFormFile> files)
        {
            var fileNames = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");

            try
            {
                // Check if the file is null or empty
                if (files == null || files.Count == 0)
                {
                    return null;
                }

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        fileNames.Add("Error: File is empty or null");
                        continue;
                    }
                   
                    var extension = Path.GetExtension(file.FileName);
                    var name = Path.GetFileName(file.FileName);
                    var fileName = DateTimeOffset.Now.Ticks.ToString()+"_"+name ;
                    var exactPath = Path.Combine(uploadPath, fileName);
                    
                    using (var fileStream = new FileStream(exactPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                  
                    fileNames.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                fileNames.Add($"Error: {ex.Message}");
            }
            // Return the list of file names
            return fileNames;
        }


        // Generates a URL for accessing a file on the server.
        public async Task<string> GetFileUrl(string fileName)
        {

            // Get the scheme (http or https), host, and path base from the current HTTP context
            var scheme = httpContextAccessor.HttpContext?.Request.Scheme ?? string.Empty; // http or https
            var host = httpContextAccessor.HttpContext?.Request.Host; //  localhost:7116
            var pathBase = httpContextAccessor.HttpContext?.Request.PathBase ?? string.Empty;

            // Construct the full URL for the file
            var fileUrl = $"{scheme}://{host}{pathBase}/{fileName}";

            return fileUrl;
        }



       



    }
}

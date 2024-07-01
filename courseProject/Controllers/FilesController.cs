using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {


        /// <summary>
        ///  download a file by its filename.
        /// </summary>
        /// <param name="filename">The name of the file to download.</param>
        /// <returns>An IActionResult representing the file download response.</returns>
        [HttpGet("DownloadFile")]
        //[Authorize]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            // Combine the current directory path with the provided filename to get the full file path.
            var filepath = Path.Combine(Directory.GetCurrentDirectory(),  filename);
            // Check if the file exists in the specified filepath.
            if (!System.IO.File.Exists(filepath))
            {
                // If the file does not exist, return a 404 Not Found response.
                return NotFound();
            }

            // Initialize a FileExtensionContentTypeProvider to determine the content type of the file.
            var provider = new FileExtensionContentTypeProvider();
            // Attempt to determine the content type of the file based on its extension.
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                // If the content type cannot be determined, default to "application/octet-stream".
                contenttype = "application/octet-stream";
            }
            // Read all bytes from the file asynchronously.
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            // Return a FileResult with the file bytes, content type, and filename for download.
            return File( bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}

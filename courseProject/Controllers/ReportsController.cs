using Microsoft.AspNetCore.Mvc;
using courseProject.Services.Reports.PDF;
using courseProject.Services.Reports.EXCEL;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.Reports;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

           
        private readonly IPdfServices pdfServices;
        private readonly IExcelServices excelServices;

        public ReportsController(IPdfServices pdfServices , IExcelServices excelServices)
            {
              
            this.pdfServices = pdfServices;
            this.excelServices = excelServices;
        }





        /// <summary>
        /// Endpoint to export data to PDF based on specified data type.
        /// </summary>
        /// <param name="reportDTO">DTO containing the data type to export.</param>
        /// <returns>An IActionResult representing the PDF file export or appropriate error responses.</returns>
        [HttpGet("export-all-Data-To-PDF")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ExportAllDataToPdf([FromQuery] ReportDTO reportDTO)
        {
            byte[] pdfBytes = null;
            string dataType = reportDTO.data.ToLower();


            switch (dataType)
            {
                case "student":
                    pdfBytes = await pdfServices.GenerateStudentsPdfAsync();
                    break;
                case "employee":
                    pdfBytes = await pdfServices.GenerateEmployeesPdfAsync();
                    break;
                case "course":
                    pdfBytes = await pdfServices.GenerateCoursePdfAsync();
                    break;
                default:
                    return BadRequest("Invalid data type specified.");
            }
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return NotFound("No Data found to export.");
            }

                // Return the PDF file
                return File(pdfBytes, "application/pdf", $"{dataType}.pdf");
            }





        /// <summary>
        /// Endpoint to export data to Excel based on specified data type.
        /// </summary>
        /// <param name="reportDTO">DTO containing the data type to export.</param>
        /// <returns>An IActionResult representing the Excel file export or appropriate error responses.</returns>
        [HttpGet("export-all-data-to-excel")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> ExportAllDataToExcel([FromQuery] ReportDTO reportDTO)
        {
            byte[] excelBytes = null;
            string dataType = reportDTO.data.ToLower();

            switch (dataType)
            {
                case "student":
                    // Generate Excel for Students
                    excelBytes = await excelServices.GenerateStudentsExcelAsync();
                    break;
                case "employee":
                    // Generate Excel for Employees
                    excelBytes = await excelServices.GenerateEmployeesExcelAsync();
                    break;
                case "course":
                    // Generate Excel for Courses
                    excelBytes = await excelServices.GenerateCourseExcelAsync();
                    break;
                default:
                    return BadRequest("Invalid data type specified.");
            }

            if (excelBytes == null || excelBytes.Length == 0)
            {
                return NotFound("No data found to export.");
            }

            // Return the Excel file
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{dataType}.xlsx");
        }
    }
















}





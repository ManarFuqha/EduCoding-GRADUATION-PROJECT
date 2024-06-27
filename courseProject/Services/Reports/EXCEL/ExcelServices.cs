using courseProject.Services.Courses;
using courseProject.Services.Employees;
using courseProject.Services.Students;
using OfficeOpenXml;

namespace courseProject.Services.Reports.EXCEL
{
    public class ExcelServices : IExcelServices
    {
        private readonly IStudentServices studentServices;
        private readonly IEmployeeServices employeeServices;
        private readonly ICourseServices courseServices;

        public ExcelServices(IStudentServices studentServices , IEmployeeServices employeeServices , ICourseServices courseServices)
        {
            this.studentServices = studentServices;
            this.employeeServices = employeeServices;
            this.courseServices = courseServices;
        }



        public async Task<byte[]> GenerateStudentsExcelAsync()
        {
            var students = await studentServices.GetAllStudents();
            if (students == null)
            {
                return null;
            }
            return GenerateExcel("Students Table", students.Select((s, index) => new[]
            {
            (index + 1).ToString(), // Numbering
            $"{s.userName} {s.LName}",
            s.email,
            s.phoneNumber,
            s.address
        }).ToList(), new[] { "#", "Name", "Email", "Phone No.", "Address" });
        }








        public async Task<byte[]> GenerateEmployeesExcelAsync()
        {
            // Fetch all employees
            var employees = await employeeServices.getAllEmployees();
            if (employees == null) return null;
            // Transform employee data and generate PDF
            return GenerateExcel("Employees Table", employees.Select((e, index) => new[]
            {
            (index + 1).ToString(), // Numbering
            $"{e.FName} {e.LName}", // Full Name
            e.email, // Email
            e.type, //Role
            e.phoneNumber, // Phone Number
            e.address // Address
        }).ToList(), new[] { "#", "Name", "Email", "Role", "Phone No.", "Address" });
        }


        public async Task<byte[]> GenerateCourseExcelAsync()
        {
            // Fetch all Courses
            var Courses = await courseServices.GetAllCoursesForAccreditAsync();
            if (Courses == null) return null;

            return GenerateExcel("Courses Table", Courses.Select((c, index) => new[]
       {
            (index + 1).ToString(), // Numbering
            c.name, 
       //     c.description, 
            c.status,
            c.price.ToString(),
            c.category ,
            c.startDate.Value.ToString("dd/MM/yyyy") ,
            c.totalHours.ToString()
        }).ToList(), new[] { "#", "Name", "Status", "Price", "Category", "Start Date", "Total Hours" });

        }


        private byte[] GenerateExcel(string title, List<string?[]> data, string[] headers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set license context
           
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add(title);
                int row = 1;

                // Add Date and Day at the top of the sheet
                worksheet.Cells[row, 1].Value = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1, row, headers.Length].Merge = true; // Merge across header columns
                row++;

                worksheet.Cells[row, 1].Value = "Day: " + DateTime.Now.DayOfWeek.ToString();
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1, row, headers.Length].Merge = true;
                row++;

                // Add title
                worksheet.Cells[row, 1].Value = title;
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1].Style.Font.Size = 14; // Increase font size
                worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1, row, headers.Length].Merge = true; // Merge across header columns
                row++;

                // Leave a blank row after the title
                row++;

                // Add headers
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[row, i + 1].Value = headers[i];
                    worksheet.Cells[row, i + 1].Style.Font.Bold = true; // Make header bold
                }

                // Add data
                foreach (var rowData in data)
                {
                    row++;
                    for (int j = 0; j < rowData.Length; j++)
                    {
                        worksheet.Cells[row, j + 1].Value = rowData[j];
                    }
                }

                // AutoFit columns for all cells
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convert the Excel package to a byte array
                return package.GetAsByteArray();
            }
        }




    }
}

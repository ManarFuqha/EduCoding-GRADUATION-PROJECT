
using courseProject.Services.Courses;
using courseProject.Services.Employees;
using courseProject.Services.Students;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace courseProject.Services.Reports.PDF
{
    public class PdfServices : IPdfServices
    {
        private readonly IStudentServices studentServices;
        private readonly IEmployeeServices employeeServices;
        private readonly ICourseServices courseServices;

        public PdfServices(IStudentServices studentServices , IEmployeeServices employeeServices , ICourseServices courseServices)
        {
            this.studentServices = studentServices;
            this.employeeServices = employeeServices;
            this.courseServices = courseServices;
        }


        public async Task<byte[]> GenerateStudentsPdfAsync()
        {
            var students = await studentServices.GetAllStudents();
            if (students == null)
            {
                return null;
            }
            return GeneratePdf("Students Table", students.Select((s, index) => new[]
            {
            (index + 1).ToString(), // Numbering
            $"{s.userName} {s.LName}",
            s.email,
            s.phoneNumber,
            s.address
        }).ToList(), new[] { "#", "Name", "Email", "Phone No.", "Address" });
        }


       





        public async Task<byte[]> GenerateEmployeesPdfAsync()
        {
            // Fetch all employees
            var employees = await employeeServices.getAllEmployees();
            if (employees == null) return null;
            // Transform employee data and generate PDF
            return GeneratePdf("Employees Table", employees.Select((e, index) => new[]
            {
            (index + 1).ToString(), // Numbering
            $"{e.FName} {e.LName}", // Full Name
            e.email, // Email
            e.type, //Role
            e.phoneNumber, // Phone Number
            e.address // Address
        }).ToList(), new[] { "#", "Name", "Email","Role", "Phone No.", "Address" });

        }


        public async Task<byte[]> GenerateCoursePdfAsync()
        {
            // Fetch all Courses
            var Courses = await courseServices.GetAllCoursesForAccreditAsync();
            if (Courses == null) return null;
            
                return GeneratePdf("Courses Table", Courses.Select((c, index) => new[]
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



        // Common method to generate PDF
        private byte[] GeneratePdf(string title, List<string?[]> data, string[] headers)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create PDF writer and document
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Add the date and day to the document
                document.Add(new Paragraph("Date : " + DateTime.Now.ToString("yyyy-MM-dd"))
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(12));
                document.Add(new Paragraph("Day : " + DateTime.Now.DayOfWeek)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(12));

                // Add the title to the document
                document.Add(new Paragraph(title)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));

                // Define column widths based on the number of headers
                float[] columnWidths;
                switch (headers.Length)
                {
                    case 5:
                        columnWidths = new float[] { 1, 4, 8, 4, 4 };
                        break;
                    case 6: 
                        columnWidths = new float[] { 1, 4, 8, 4, 4, 4 };
                        break;
                    case 7:
                        columnWidths = new float[] { 1, 4, 4 ,3, 4, 4, 3 };
                        break;
                    default:
                        columnWidths =new float[headers.Length];
                        break;

                }

                Table table = new Table(UnitValue.CreatePercentArray(columnWidths));
                table.SetMarginTop(40);

                // Add header cells
                foreach (var header in headers)
                {
                    table.AddHeaderCell(header);
                }

                // Add data rows
                foreach (var row in data)
                {
                    foreach (var cell in row)
                    {
                        table.AddCell(cell ?? string.Empty); // Ensure no null values
                    }
                }

                // Add the table to the document
                document.Add(table);
                // Close the document
                document.Close();

                // Return the PDF as a byte array
                return memoryStream.ToArray();
            }
        }





       



    }
}

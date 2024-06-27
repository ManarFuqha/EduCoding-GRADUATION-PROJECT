namespace courseProject.Services.Reports.PDF
{
    public interface IPdfServices
    {

        public Task<byte[]> GenerateStudentsPdfAsync();
        public Task<byte[]> GenerateEmployeesPdfAsync();
        public Task<byte[]> GenerateCoursePdfAsync();
    }
}

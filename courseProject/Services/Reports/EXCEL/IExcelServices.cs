namespace courseProject.Services.Reports.EXCEL
{
    public interface IExcelServices
    {

        public Task<byte[]> GenerateStudentsExcelAsync();
        public Task<byte[]> GenerateEmployeesExcelAsync();
        public Task<byte[]> GenerateCourseExcelAsync();

    }
}

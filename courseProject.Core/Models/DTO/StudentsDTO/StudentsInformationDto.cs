namespace courseProject.Core.Models.DTO.StudentsDTO
{
    public class StudentsInformationDto
    {

        public Guid StudentId { get; set; }
        public string userName { get; set; }
        public string? LName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string? gender { get; set; }
        public string? address { get; set; }
        public string? ImageUrl { get; set; }



    }
}

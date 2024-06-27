using AutoMapper;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models;

namespace courseProject.MappingProfile
{
    public class MappingForStudentCourses : Profile
    {
        public MappingForStudentCourses()
        {

            CreateMap<StudentCourseDTO, StudentCourse>();
            CreateMap<StudentCustomCourseDTO, Request>();


        }
    }
}

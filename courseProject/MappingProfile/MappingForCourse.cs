using AutoMapper;
using courseProject.Common;
using courseProject.core.Models;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.MaterialsDTO;

namespace courseProject.MappingProfile
{
    public class MappingForCourse : Profile
    {
      

       
        public MappingForCourse()
        {
           

            CreateMap<Course, CourseInformationDto>()
                .ForMember(x => x.InstructorName, o => o.MapFrom(y => y.instructor.userName + " " + y.instructor.LName))
                .ForMember(x => x.SubAdminName, o => o.MapFrom(y => y.subAdmin.userName + " " + y.subAdmin.LName))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.Deadline, o => o.MapFrom(y => y.Deadline.HasValue ? y.Deadline.Value.ToString("dd/MM/yyyy") : null));
            
                

            CreateMap<Course, CourseInfoForStudentsDTO>()
                .ForMember(x => x.InstructorName, o => o.MapFrom(y => y.instructor.userName + " " + y.instructor.LName))
                .ForMember(x => x.SubAdminName, o => o.MapFrom(y => y.subAdmin.userName + " " + y.subAdmin.LName))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.Deadline, o => o.MapFrom(y => y.Deadline.HasValue ? y.Deadline.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.isEnrolled, o => o.MapFrom(y => y.studentCourses.Any(x => x.isEnrolled)));

            CreateMap<Course, CourseAccreditDTO>()
                .ForMember(x => x.SubAdminFName, o => o.MapFrom(y => y.subAdmin.userName))
                .ForMember(x => x.SubAdminLName, o => o.MapFrom(y => y.subAdmin.LName))
                .ForMember(x => x.InstructorFName, o => o.MapFrom(y => y.instructor.userName))
                .ForMember(x => x.InstructorLName, o => o.MapFrom(y => y.instructor.LName))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy") : null));
           

            CreateMap<CourseForCreateDTO, Course>();
            CreateMap<CourseForCreateDTO, Request>()
                .ForMember(x => x.name , o=>o.MapFrom(y=>y.name));

                    

            CreateMap<CourseForEditDTO, Course>()         
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping .IsNotDefault(srcMember)));
            ;

            CreateMap<SubmissionsDTO, Student_Task_Submissions>();

            CreateMap<Request, CustomCourseForRetriveDTO>()
                .ForMember(x => x.StudentFName, o => o.MapFrom(y => y.student.userName))
                .ForMember(x => x.StudentLName, o => o.MapFrom(y => y.student.LName))      
                .ForMember(x => x.startDate, o => o.MapFrom(y => y.startDate.HasValue ? y.startDate.Value.ToString("dd/MM/yyyy" ) :null))
                .ForMember(x => x.endDate, o => o.MapFrom(y => y.endDate.HasValue ? y.endDate.Value.ToString("dd/MM/yyyy") : null));

            CreateMap<EditCourseAfterAccreditDTO, Course>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));


           
          
        }
    }
}

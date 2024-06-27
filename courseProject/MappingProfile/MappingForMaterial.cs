using AutoMapper;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Core.Models;
using courseProject.Common;

namespace courseProject.MappingProfile
{
    public class MappingForMaterial : Profile
    {
        public MappingForMaterial()
        {

            CreateMap<CourseMaterialDTO, CourseMaterial>();
            CreateMap<TaskDTO, CourseMaterial>();
            CreateMap<TaskForEditDTO, CourseMaterial>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<FileDTO, CourseMaterial>();

            CreateMap<FileToEditDTO, CourseMaterial>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
           IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<AnnouncementDTO, CourseMaterial>();
            CreateMap<AnnouncementForEditDTO, CourseMaterial>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
         IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
            CreateMap<LinkDTO, CourseMaterial>();
            CreateMap<LinkForEditDTO, CourseMaterial>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
         IsNotDefaultClassForMapping.IsNotDefault(srcMember)));

        }
    }
}

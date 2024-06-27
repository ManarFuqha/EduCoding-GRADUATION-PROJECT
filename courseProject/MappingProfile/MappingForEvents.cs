using AutoMapper;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.Common;

namespace courseProject.MappingProfile
{
    public class MappingForEvents : Profile
    {
        public MappingForEvents()
        {
            CreateMap<Event , EventDto>()
                .ForMember(x=>x.subAdminName , o=>o.MapFrom(y=>y.subAdmin.userName))              
                .ForMember(x => x.dateOfEvent, o => o.MapFrom(y => y.dateOfEvent.HasValue ? y.dateOfEvent.Value.ToString("dd/MM/yyyy") : null));

            CreateMap<EventForCreateDTO, Event>();
            CreateMap<EventForCreateDTO, Request>()
             .ForMember(x => x.name, o => o.MapFrom(y => y.name));
            //CreateMap<Request, Event>()
            //    .ForMember(x => x.requestId, o => o.MapFrom(y => y.Id));

            CreateMap<Event, EventAccreditDto>()
                .ForMember(x => x.subAdminFName, o => o.MapFrom(y => y.subAdmin.userName))
                .ForMember(x => x.dateOfEvent, o => o.MapFrom(y => y.dateOfEvent.HasValue ? y.dateOfEvent.Value.ToString("dd/MM/yyyy") : null));

            CreateMap<EventForEditDTO, Event>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) =>
            IsNotDefaultClassForMapping.IsNotDefault(srcMember)));
        }
    }
}

using courseProject.Core.Models;
using courseProject.Core.Models.DTO.MaterialsDTO;
using ErrorOr;
using System.Collections;
namespace courseProject.Services.Materials
{
    public interface IMaterialServices
    {

        public Task<ErrorOr<Created>> AddTask(TaskDTO taskDTO);
        public Task<ErrorOr<Created>> AddFile(FileDTO fileDTO);
        public Task<ErrorOr<Created>> AddAnnouncement(AnnouncementDTO AnnouncementDTO);
        public Task<ErrorOr<Created>> AddLink(LinkDTO linkDTO);
        public Task<ErrorOr<Updated>> EditTask(Guid id, TaskForEditDTO taskDTO);
        public Task<ErrorOr<Updated>> EditFile(Guid id, FileToEditDTO fileDTO);
        public Task<ErrorOr<Updated>> EditAnnouncement(Guid id, AnnouncementForEditDTO AnnouncementDTO);
        public Task<ErrorOr<Updated>> EDitLink(Guid id, LinkForEditDTO linkDTO);
        public Task<ErrorOr<Deleted>> DeleteMaterial(Guid id);
        public Task<ErrorOr<CourseMaterial>> GetMaterialById(Guid id);
    
        public Task<ErrorOr<Deleted>> deleteFiles(Guid materialId);
        public Task<ErrorOr<Updated>> changeMaterialStatus(Guid materialId ,bool isHidden);


        public Task<ErrorOr<IReadOnlyList<CourseMaterial>>> GetAllMaterialInTheCourse(Guid? courseId, Guid? consultationId);

    }
}

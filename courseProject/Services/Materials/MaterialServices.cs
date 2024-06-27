using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using courseProject.core.Models;

namespace courseProject.Services.Materials
{
    public class MaterialServices : IMaterialServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        

        public MaterialServices(IUnitOfWork unitOfWork , IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
       
        }

    

        public async Task<ErrorOr<Created>> AddTask(TaskDTO taskDTO)
        {
           var uploadedFileNames=await unitOfWork.FileRepository.UploadFiles(taskDTO.pdf);
            var taskMapped = mapper.Map<TaskDTO, CourseMaterial>(taskDTO);
            taskMapped.type = "Task";            
            var getcourses = await unitOfWork.CourseRepository.GetAllCoursesGivenByInstructorIdAsync(taskDTO.InstructorId);
            var getConsultations = await unitOfWork.RequestRepository.GetAllConsultationRequestByInstructorIdAsync(taskDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == taskDTO.courseId) && !getConsultations.Any(x => x.Id == taskDTO.consultationId))
            {
                return ErrorUser.Unauthorized;
            }
            await unitOfWork.materialRepository.AddMaterial(taskMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            MaterialFiles materialFiles = new MaterialFiles();
            materialFiles.materialId = taskMapped.Id;
            foreach (var file in uploadedFileNames)
            {
                materialFiles.pdfUrl = "Files\\" + file;
                await unitOfWork.materialRepository.AddMaterialFiles(materialFiles);
                await unitOfWork.materialRepository.saveAsync();
            }
          
            return Result.Created;
        }


        public async Task<ErrorOr<Created>> AddFile(FileDTO fileDTO)
        {
            var uploadedFileNames = await unitOfWork.FileRepository.UploadFiles(fileDTO.pdf);
            var fileMapped = mapper.Map<FileDTO, CourseMaterial>(fileDTO);
            fileMapped.type = "File";
            var getcourses = await unitOfWork.CourseRepository.GetAllCoursesGivenByInstructorIdAsync(fileDTO.InstructorId);
            var getConsultations = await unitOfWork.RequestRepository.GetAllConsultationRequestByInstructorIdAsync(fileDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == fileDTO.courseId) && !getConsultations.Any(x => x.Id == fileDTO.consultationId))
                return ErrorUser.Unauthorized;
   
            await unitOfWork.materialRepository.AddMaterial(fileMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            MaterialFiles materialFiles = new MaterialFiles();
            materialFiles.materialId = fileMapped.Id;
            foreach (var file in uploadedFileNames)
            {
                materialFiles.pdfUrl = "Files\\" + file;
                await unitOfWork.materialRepository.AddMaterialFiles(materialFiles);
                await unitOfWork.materialRepository.saveAsync();
            }
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddAnnouncement(AnnouncementDTO AnnouncementDTO)
        {
            var AnnouncementMapped = mapper.Map<AnnouncementDTO, CourseMaterial>(AnnouncementDTO);
            AnnouncementMapped.type = "Announcement";
            var getcourses = await unitOfWork.CourseRepository.GetAllCoursesGivenByInstructorIdAsync(AnnouncementDTO.InstructorId);
            var getConsultations = await unitOfWork.RequestRepository.GetAllConsultationRequestByInstructorIdAsync(AnnouncementDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == AnnouncementDTO.courseId) && !getConsultations.Any(x => x.Id == AnnouncementDTO.consultationId))
                return ErrorUser.Unauthorized;
            await unitOfWork.materialRepository.AddMaterial(AnnouncementMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddLink(LinkDTO linkDTO)
        {
            var linkMapped = mapper.Map<LinkDTO, CourseMaterial>(linkDTO);
            linkMapped.type = "Link";
            var getcourses = await unitOfWork.CourseRepository.GetAllCoursesGivenByInstructorIdAsync(linkDTO.InstructorId);
            var getConsultations = await unitOfWork.RequestRepository.GetAllConsultationRequestByInstructorIdAsync(linkDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == linkDTO.courseId) && !getConsultations.Any(x => x.Id == linkDTO.consultationId))
                return ErrorUser.Unauthorized;
            await unitOfWork.materialRepository.AddMaterial(linkMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> EditTask(Guid id, TaskForEditDTO taskDTO)
        {
            
            var TaskToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (TaskToUpdate == null) return ErrorMaterial.NotFound;
            

            var Taskmapper = mapper.Map(taskDTO, TaskToUpdate);
            Taskmapper.Id = id;
            Taskmapper.type = "Task";       
            
            await unitOfWork.materialRepository.EditMaterial(Taskmapper);
            var success1 = await unitOfWork.instructorRepositpry.saveAsync();

            if (taskDTO.pdf != null && taskDTO.pdf.Count() > 0)
            {
                var uploadedFileNames = await unitOfWork.FileRepository.UploadFiles(taskDTO.pdf);
              
                foreach (var file in uploadedFileNames)
                {
                    var newMaterialFile = new MaterialFiles
                    {
                        materialId = id,
                        pdfUrl = "Files\\" + file
                    };

                  
                    await unitOfWork.materialRepository.AddMaterialFiles(newMaterialFile);
                    await unitOfWork.materialRepository.saveAsync();

                }

                

            }


            
            return Result.Updated;
        }


       





            public async Task<ErrorOr<Updated>> EditFile(Guid id, FileToEditDTO fileDTO)
        {
           
            var FileToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (FileToUpdate == null) return ErrorMaterial.NotFound;
           

            var filemapper = mapper.Map(fileDTO, FileToUpdate);
            filemapper.Id = id;
            filemapper.type = "File";
            await unitOfWork.materialRepository.EditMaterial(filemapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();

            if (fileDTO.pdf != null && fileDTO.pdf.Count() > 0)
            {
                var uploadedFileNames = await unitOfWork.FileRepository.UploadFiles(fileDTO.pdf);

                foreach (var file in uploadedFileNames)
                {
                    var newMaterialFile = new MaterialFiles
                    {
                        materialId = id,
                        pdfUrl = "Files\\" + file
                    };

                    await unitOfWork.materialRepository.AddMaterialFiles(newMaterialFile);
                    await unitOfWork.materialRepository.saveAsync();

                }

            }
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EditAnnouncement(Guid id, AnnouncementForEditDTO AnnouncementDTO)
        {
           
            var AnnouncementToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (AnnouncementToUpdate == null) return ErrorMaterial.NotFound;
           

            var Announcementmapper = mapper.Map(AnnouncementDTO, AnnouncementToUpdate);
            Announcementmapper.Id = id;
            Announcementmapper.type = "Announcement";
            await unitOfWork.materialRepository.EditMaterial(Announcementmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EDitLink(Guid id, LinkForEditDTO linkDTO)
        {
           
            var LinkToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (LinkToUpdate == null) return ErrorMaterial.NotFound;
            

            var Linkmapper = mapper.Map(linkDTO, LinkToUpdate);
            Linkmapper.Id = id;
            Linkmapper.type = "Link";
            await unitOfWork.materialRepository.EditMaterial(Linkmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> DeleteMaterial(Guid id)
        {
            var materail = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (materail == null) return ErrorMaterial.NotFound;
            
            await unitOfWork.materialRepository.DeleteMaterial(id);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<CourseMaterial>> GetMaterialById(Guid id)
        {
            var material = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (material == null) return ErrorMaterial.NotFound;
            var user = await unitOfWork.UserRepository.getRoleFromToken();
            if(user == null) return ErrorUser.NotFound;
            if(user.role.ToLower()=="student")
            {
                var submission = material.Student_Task_Submissions.FirstOrDefault(x => x.StudentId==user.UserId && x.TaskId==id);
                if (submission == null) material.Student_Task_Submissions = null;
                else
                {
                    submission.pdfUrl = await unitOfWork.FileRepository.GetFileUrl(submission.pdfUrl);
                    material.Student_Task_Submissions = new List<Student_Task_Submissions> { submission };

                }
            }
            if (material.MaterialFiles != null && material.MaterialFiles.Any())
            {
                foreach (var file in material.MaterialFiles)
                {
                    if (!string.IsNullOrEmpty(file.pdfUrl))
                    {
                        file.pdfUrl = await unitOfWork.FileRepository.GetFileUrl(file.pdfUrl);
                    }
                }
            }
           
            return material;           
        }

        public async Task<ErrorOr<Deleted>> deleteFiles(Guid materialId)
        {
            var getFiles = await unitOfWork.materialRepository.GetMaterialFilesByMaterialId(materialId);
            foreach (var file in getFiles)
            {
                await unitOfWork.materialRepository.DeleteFilesById(file);
                await unitOfWork.materialRepository.saveAsync();

            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<Updated>> changeMaterialStatus(Guid materialId, bool isHidden)
        {
            var getmaterial = await unitOfWork.materialRepository.GetMaterialByIdAsync(materialId);
            if (getmaterial == null) return ErrorMaterial.NotFound;

            getmaterial.isHidden = isHidden;
            await unitOfWork.materialRepository.EditMaterial(getmaterial);
            await unitOfWork.materialRepository.saveAsync();
            return Result.Updated;
        }



        
        public async Task<ErrorOr<IReadOnlyList<CourseMaterial>>> GetAllMaterialInTheCourse(Guid? courseId, Guid? consultationId)
        {
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (getCourse == null && courseId != null) return ErrorCourse.NotFound;
            var getConsultation = await unitOfWork.lecturesRepository.GetConsultationById(consultationId);
            if (getConsultation == null && consultationId != null) return ErrorLectures.NotFound;
            var user = await unitOfWork.UserRepository.getRoleFromToken();
            if(user==null) return ErrorUser.NotFound;
            var AllMaterials = await unitOfWork.materialRepository.GetAllMaterial(courseId, consultationId, user.role);
            
            

            foreach (var material in AllMaterials)
            {
                foreach (var file in material.MaterialFiles)
                {
                    if (!string.IsNullOrEmpty(file.pdfUrl))
                    {
                        file.pdfUrl = await unitOfWork.FileRepository.GetFileUrl(file.pdfUrl);
                    }
                }
               
            }
            return AllMaterials.ToErrorOr();
        }

    }
}

using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.ServiceErrors;
using ErrorOr;


namespace courseProject.Services.Skill
{
    public class SkillsServices :ISkillsServices
    {
        private readonly IUnitOfWork unitOfWork;

        public SkillsServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> AddSkillByAdmin(string skillName)
        {
            var getAllSkills = await unitOfWork.skillRepository.GetAllSkillsAsync();
          

            if (getAllSkills.Any(x => x.name == skillName)) return ErrorSkills.ChapterNameExists;

            Skills skills = new Skills();
            skills.name = skillName;
            await unitOfWork.skillRepository.addSkillOptionsAsync(skills);
            await unitOfWork.skillRepository.saveAsync();
            return skillName;


        }

        public async Task<ErrorOr<Created>> chooseANewSkillToInstructor(Guid instructorId, ListIntegerDTO array)
        {
            var FoundInstrutor = await unitOfWork.UserRepository.ViewProfileAsync(instructorId , "instructor");
            if (FoundInstrutor == null)
            {
                return ErrorInstructor.NotFound;
            }
            if (array == null || array.skills.Count() == 0)
            {
                return ErrorSkills.NoContent;
            }
            await unitOfWork.skillRepository.AddListOfSkillsAsync(instructorId, array.skills);

             await unitOfWork.skillRepository.GetAllSkillsNameToInstructorAsync(array.skills);
            return  Result.Created;
        }

       

        public async Task<IReadOnlyList< Skills>> getAllSkillesAddedByAdmin()
        {
            var allSkills = await unitOfWork.skillRepository.GetAllSkillsAsync();
            allSkills = allSkills.OrderByDescending(x => x.TimeOfAdded).ToList();
            return allSkills;
        }

        public async Task<ErrorOr<IReadOnlyList<Skills>>> getAllSkillOptionsToInstructor(Guid instructorId)
        {
            var getinstructor = await unitOfWork.instructorRepositpry.getInstructorById(instructorId);
            if (getinstructor == null) return ErrorInstructor.NotFound;
            var allSkills = await unitOfWork.skillRepository.getAllUnregisteredSkillsOfTheInstructor(instructorId);
            return allSkills.ToErrorOr();
            

        }

        public async Task<ErrorOr<Deleted>> DeleteAnInstructorSkill(Guid InstructorId, Guid SkillId)
        {
            var getAllSkills = await unitOfWork.skillRepository.GetAllSkillsAsync();
            if (!getAllSkills.Any(x => x.Id == SkillId))
            {
                return ErrorSkills.NotFound;
            }
            var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(InstructorId, "instructor");
            if (instructorFound == null) return ErrorInstructor.NotFound;

            var getInstructorSkillsRecords = await unitOfWork.instructorRepositpry.GetAllInstructorSkillsRecoredsAsync();
            if (!getInstructorSkillsRecords.Any(x => x.InstructorId == InstructorId && x.skillId == SkillId))
            {   
                return ErrorSkills.NotHasSkill;
            }
           
            InstructorSkills instructorSkills = new InstructorSkills();
            instructorSkills.InstructorId = InstructorId;
            instructorSkills.skillId = SkillId;
            await unitOfWork.skillRepository.RemoveASkill(instructorSkills);
            await unitOfWork.instructorRepositpry.saveAsync();

            return Result.Deleted;
            
            
        }

        public async Task<ErrorOr< IReadOnlyList<Skills>>> GetAllInstructorSkills(Guid instructorId)
        {
            var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(instructorId, "instructor");
            if (instructorFound == null) return ErrorInstructor.NotFound;

            var getAllHisSkills = await unitOfWork.skillRepository.getAllInstructorSkills(instructorId);
            
            return getAllHisSkills.ToErrorOr();
        }
    }
}

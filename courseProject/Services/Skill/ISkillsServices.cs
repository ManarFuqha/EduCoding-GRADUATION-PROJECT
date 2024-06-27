using courseProject.Core.Models;
using courseProject.Core.Models.DTO.InstructorsDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace courseProject.Services.Skill
{
    public interface ISkillsServices
    {
        public Task<ErrorOr<string>> AddSkillByAdmin(string skillName);
        public Task<IReadOnlyList<Skills>> getAllSkillesAddedByAdmin();
        public Task<ErrorOr<IReadOnlyList<Skills>>> getAllSkillOptionsToInstructor(Guid instructorId);
        public Task<ErrorOr<Created>> chooseANewSkillToInstructor(Guid instructorId,  ListIntegerDTO array); 
        public Task <ErrorOr<Deleted>> DeleteAnInstructorSkill(Guid InstructorId , Guid SkillId);
        public Task<ErrorOr<IReadOnlyList<Skills>>> GetAllInstructorSkills(Guid instructorId);
    }
}

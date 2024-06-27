using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ISkillRepository :IGenericRepository1<Skills>
    {

        public Task addSkillOptionsAsync(Skills skill);
        public Task<IReadOnlyList<Skills>> GetAllSkillsAsync();
        public Task AddListOfSkillsAsync(Guid instructorId, List<Guid> skills);
        public Task<IReadOnlyList<string>> GetAllSkillsNameToInstructorAsync(List<Guid> skills);
        public Task RemoveASkill(InstructorSkills instructorSkills);
        public Task<IReadOnlyList<Skills>> getAllUnregisteredSkillsOfTheInstructor(Guid instructorId);
        public Task<IReadOnlyList<Skills>> getAllInstructorSkills(Guid instructorId);

    }
}

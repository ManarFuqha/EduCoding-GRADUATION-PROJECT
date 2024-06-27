using Microsoft.EntityFrameworkCore;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;


namespace courseProject.Repository.GenericRepository
{
    public class instructorRepositpry : GenericRepository1<User>, IinstructorRepositpry
    {
        private readonly projectDbContext dbContext;

        public instructorRepositpry(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

       
       

        // add a new office hours
        public async Task AddOfficeHoursAsync(Instructor_Working_Hours instructor_Working_Hours)
        {
            await dbContext.Set<Instructor_Working_Hours>().AddAsync(instructor_Working_Hours);
        }

        public async Task<IReadOnlyList<Instructor_Working_Hours>> GetOfficeHourByIdAsync(Guid instructorId)
        {
            return  await dbContext.Instructor_Working_Hours.Where(x=>x.InstructorId == instructorId).ToListAsync();
        }

            

        //public async Task<Instructor> getInstructorByIdAsync(Guid id)
        //{
        //    return await dbContext.instructors.Include(x=>x.user).FirstOrDefaultAsync(x => x.InstructorId == id);
        //}

        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAllInstructorsOfficeHoursAsync()
        {
           return await dbContext.Instructor_Working_Hours.Include(x=>x.instructor).ToListAsync();
        }

       
        // get all skills selected by all instructors 
        public async Task<IReadOnlyList<InstructorSkills>> GetAllInstructorSkillsRecoredsAsync()
        {
          return  await dbContext.InstructorSkills.ToListAsync();
        }

       
        // get a list of instructor depends on inputs when student added to book a lecture
        public async Task<IReadOnlyList<Instructor_Working_Hours>> getAListOfInstructorDependOnSkillsAndOfficeTime(Guid skillID, TimeSpan startTime, TimeSpan endTime, DateTime date )
        {
            return await dbContext.Instructor_Working_Hours.Include(x => x.instructor).ThenInclude(x => x.consultations)
                .Include(x=>x.instructor)
                
                .Where(x => x.day == date.DayOfWeek)
                .Where(x => x.startTime <= startTime && x.endTime >= endTime)
                .Where(x=>x.instructor.instructorSkills.Any(y=>y.skillId ==skillID))
                .Where(x => !x.instructor.consultations.Any(y =>
        y.date == date.Date && (
            (startTime >= y.startTime && startTime < y.endTime) ||
            (endTime > y.startTime && endTime <= y.endTime) ||
            (startTime <= y.startTime && endTime >= y.endTime)
        )))
    .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> getAllInstructors()
        {
            return await dbContext.users.Where(x => x.role.ToLower() == "instructor").ToListAsync();
        }

        public async Task<User> getInstructorById(Guid instructorId)
        {
           return  await dbContext.users.Where(x => x.role.ToLower() == "instructor").FirstOrDefaultAsync(x=>x.UserId==instructorId);
        }
        //public async Task createInstructorAccountAsync(Instructor entity)
        //{
        //    await dbContext.Set<Instructor>().AddAsync(entity);
        //}

    }
}

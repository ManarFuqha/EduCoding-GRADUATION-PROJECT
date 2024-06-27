using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;




namespace courseProject.Repository.GenericRepository
{
    public class CourseRepository : GenericRepository1<Course>, ICourseRepository<Course>
    {
        private readonly projectDbContext dbContext;
    

        public CourseRepository(projectDbContext dbContext ) : base(dbContext)
        {
            this.dbContext = dbContext;
        
        }

        

        //get a course by it's Id
        public async Task<Course> GetCourseByIdAsync(Guid? id)
        {
           return  await dbContext.courses.Include(x=>x.instructor).Include(x=>x.subAdmin)
                
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        //get total student number in a course by it's Id
        public async Task<int> GetNumberOfStudentsInTHeCourseAsync(Guid courseId)
        {
           return   dbContext.studentCourses.Where(x => x.courseId == courseId).Count();
        }

     

       // get all course created by subAdmin by it's Id , before accredit it by admin

        public async Task<IReadOnlyList<Course>> GetAllUndefinedCoursesBySubAdminIdAsync(Guid subAdminId)
        {
            return await dbContext.courses.Include(x=>x.instructor).Include(x => x.subAdmin)

                .Where(x=>x.subAdminId == subAdminId && x.status.ToLower()=="undefined")
                .OrderByDescending(x=>x.dateOfAdded)
                .ToListAsync();
        }


        // get a accredit courses by it's Id , stutus => { accredit , start , finish}
        public async Task<Course> getAccreditCourseByIdAcync(Guid courseId)
        {
            return await dbContext.courses.Where(x => x.status.ToLower() != "undefined" || x.status.ToLower() != "reject").FirstOrDefaultAsync(x => x.Id == courseId);
        }


        // get all courses given by istructor by it's id , and after accrefit it 
        public async Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid)
        {
            return await dbContext.courses.Where(x => x.InstructorId == Instructorid && (x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish"))
                .OrderByDescending(x => x.dateOfAdded)
                .ToListAsync();
        }



        // get all enrolled courses by student
        public async Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid)
        {
            return await dbContext.studentCourses.Include(x => x.Course).Where(x => x.StudentId == Studentid && x.status.ToLower() == "joind")
                .OrderByDescending(x=>x.EnrollDate)
                .ToListAsync();
        }


        // get all courses to view it in courses to student , and to show if he can enroll in it , or not , or already enroll it 
        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId)
        {

            // Fetch all courses from the database that are not undefined or rejected
            var allCourses = await dbContext.courses
                .Where(x => x.status.ToLower() != "undefined" && x.status.ToLower()!="reject")
                
                .Include(x => x.studentCourses)
                .Include(x => x.instructor)
                
                .OrderByDescending(x=>x.dateOfAdded)
                .ToListAsync();


            // Check if any courses are retrieved
            if (allCourses.Any())
            {

                StudentCourse studentCourse;
                int studentCourseCount;

                // Iterate over each course to determine its availability and enrollment status
                foreach (var course in allCourses)
                {
                    studentCourse = course.studentCourses.FirstOrDefault(x => x.StudentId == studentId && course.Id == x.courseId);
                    studentCourseCount = course.studentCourses.Count();


                    // If the student is enrolled in the course, mark it as enrolled
                    if (studentCourse != null &&( course.studentCourses.Any(x => x.courseId == course.Id && x.StudentId == studentId) ))
                    {

                        studentCourse.isEnrolled = true;
                    }

                   
                        // If the course deadline has passed or the course limit is reached and the course is not finished, mark it as unavailable
                        if (((course.Deadline.Value < DateTime.Now.Date) ||course.limitNumberOfStudnet <= studentCourseCount) && course.status.ToLower()!="finish")
                        {

                            course.isAvailable = false;
                        }
                        else
                        {
                            course.isAvailable = true;
                        }
                    
                }
            }
            return allCourses;
        }



        
        public async Task CreateCourse(Course model)
        {
            await dbContext.Set<Course>().AddAsync(model);

        }
        public async Task updateCourse(Course course)
        {
            dbContext.Entry(course).State = EntityState.Modified;
        }



        // Retrieve all courses with specific statuses : accredit , start or finish (A_S_F)
        public async Task<IReadOnlyList<Course>> GetAllCoursesAsync()
        {
           

                return await dbContext.courses
                    .Where(x => x.status.ToLower() == "accredit" || x.status.ToLower() == "start" || x.status.ToLower() == "finish")
                    .Include(x => x.instructor).Include(x => x.subAdmin)
                    .OrderByDescending(x=>x.dateOfAdded)
                    .ToListAsync();
           
           
           
        }


        //return all courses with all status
        public async Task<IReadOnlyList<Course>> GetAllCoursesForAccreditAsync()
        {
            
                return await dbContext.courses
                    .Include(x => x.instructor).Include(x => x.subAdmin).OrderByDescending(x => x.dateOfAdded).ToListAsync();
           
        }

    }
}

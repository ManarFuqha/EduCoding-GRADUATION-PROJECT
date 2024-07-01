using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Services.Courses;



namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseContraller : ControllerBase
    {
        private readonly ICourseServices courseServices;    
        private readonly IMapper mapper;
        protected ApiResponce responce;

   

        public CourseContraller(ICourseServices courseServices, IMapper mapper)
        {
            this.courseServices = courseServices;          
            this.mapper = mapper;
            responce = new ApiResponce();
        
        }
        /// <summary>
        /// Retrieves all courses where status = "accredit" or "start" or "finish"
        /// </summary>
        [HttpGet("GetAllAccreditCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllCoursesAsync([FromQuery] PaginationRequest paginationRequest)
        {
           
            var getCourses = await courseServices.GetAllCourses();
          
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(getCourses);          
            responce.Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result;
            return Ok(responce);
        }



        /// <summary>
        /// Retrieves all courses available for a student to enroll in, including courses the student is already enrolled in.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="paginationRequest">Pagination parameters for the course list.</param>
        /// <returns>
        /// An IActionResult containing a paginated list of courses available for the student.
        /// </returns>
        /// <response code="200">Returns the paginated list of courses.</response>
        /// <response code="404">If the requested resource is not found.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpGet("GetAllCoursesToStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> GetAllCoursesToStudent(Guid studentId, [FromQuery] PaginationRequest paginationRequest)
        {

                var courses = await courseServices.GetAllCoursesToStudent(studentId);
                var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInfoForStudentsDTO>>(courses);
            return Ok( new ApiResponce { Result = (Pagination<CourseInfoForStudentsDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
                            
        }


        /// <summary>
        /// Retrieves all courses whit all status
        /// </summary>
        [HttpGet("GetAllCoursesForAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
         
        public async Task<IActionResult> GetAllCoursesForAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var courses = await courseServices.GetAllCoursesForAccreditAsync();
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseAccreditDTO>>(courses);
            return Ok( new ApiResponce { Result = (Pagination<CourseAccreditDTO>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });           
            
        }



        /// <summary>
        /// Creates a new course. This action can be performed by a SubAdmin or Main-SubAdmin.
        /// </summary>
        /// <param name="model">The DTO containing the course details to create.</param>
        /// <param name="StudentId">Optional Student ID associated with the course creation.</param>
        /// <returns>An IActionResult indicating the outcome of the course creation.</returns>
        [HttpPost("CreateCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]

        public async Task<IActionResult> createCourse([FromForm] CourseForCreateDTO model)
        {
         

            var courseMapped = mapper.Map<Course>(model);
            

            var createCourse = await courseServices.createCourse(courseMapped);
            if (createCourse.IsError) return NotFound( new ApiResponce { ErrorMassages =  createCourse.FirstError.Description });
            return Ok( new ApiResponce { Result="The Course Is Created Successfully"});
            

        }




        /// <summary>
        /// Changes the status of a course. This action can be performed by an Admin or Instructor.
        /// </summary>
        /// <param name="courseId">The ID of the course to update.</param>
        /// <param name="courseStatus">The new status of the course (reject, accredit, finish, start).</param>
        /// <returns>An IActionResult indicating the outcome of the status update.</returns>
        [HttpPatch("accreditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Instructor")]

        // this to change the status of courses to reject , accredit , finish , start
        public async Task<IActionResult> EditCourseStatus(Guid courseId, CourseStatusDTO courseStatus)
        {
            var updateStatus = await courseServices.accreditCourse(courseId, courseStatus.Status);
            if (updateStatus.IsError) return NotFound(new ApiResponce
            {
                ErrorMassages = updateStatus.FirstError.Description
            }) ;
            return Ok(new ApiResponce
            {
                Result = $"The Course is {courseStatus.Status}"
            });
        }


        /// <summary>
        /// Allows an admin to edit a course after it has been accredited.
        /// </summary>
        /// <param name="courseId">The ID of the course to edit.</param>
        /// <param name="editedCourse">The DTO containing the edited course details.</param>
        /// <returns>An IActionResult indicating the outcome of the course edit.</returns>
        [HttpPut("EditOnCourseAfterAccredit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        
        public async Task<IActionResult> EditOnCourseAfterAccreditByAdmin(Guid courseId, [FromForm] EditCourseAfterAccreditDTO editedCourse)
        {

            var courseService = await courseServices.EditOnCourseAfterAccredit(courseId, editedCourse);
           
            if (courseService.IsError == true) responce.ErrorMassages = courseService.FirstError.Description;
            return Ok(new ApiResponce { Result="course updated successfully"});

        }



        /// <summary>
        /// Retrieves a course by its ID.
        /// </summary>
        [HttpGet("GetCourseById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {

            var course = await courseServices.GetCourseById(id);
            if (course.IsError) return NotFound(new ApiResponce { ErrorMassages =  course.FirstError.Description });                           
            return Ok(new ApiResponce { Result = mapper.Map<Course, CourseInformationDto>(course.Value) });
        }




        /// <summary>
        /// Allows a SubAdmin or Main-SubAdmin to edit a course before it has been accredited or rejected by an admin.
        /// </summary>
        [HttpPut("EditCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]

        // edit course by who created before the admin accredit or reject the course
        public async Task<IActionResult> EditCourseBeforeAccredit(Guid id,[FromForm] CourseForEditDTO course)
        {
          
            
            var editCourse = await courseServices.EditOnCOurseBeforeAnAccredit(id , course);
            if (editCourse.IsError) return NotFound(new ApiResponce { ErrorMassages =  editCourse.FirstError.Description  });
            return Ok(new ApiResponce { Result = "The course is updated successfully" });
            
        }


        /// <summary>
        /// Retrieves all undefined courses created by a SubAdmin based on their ID.
        /// </summary>
        [HttpGet("GetallUndefinedCoursesToSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]
        public async Task<IActionResult> GetALlUndefinedCoursesForSubAdmins(Guid subAdminId , [FromQuery] PaginationRequest paginationRequest)
        {          
                var getCourses = await courseServices.GetALlUndefinedCoursesForSubAdmins(subAdminId);
            if(getCourses.IsError) return NotFound(new ApiResponce { ErrorMassages =  getCourses.FirstError.Description  });
            var mapperCourse = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseInformationDto>>(getCourses.Value);
                return Ok(new ApiResponce { Result = (Pagination<CourseInformationDto>.CreateAsync(mapperCourse, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });                          
        }





        /// <summary>
        /// Retrieves all courses given by a specific instructor, paginated.
        /// </summary>
        /// <param name="Instructorid">The ID of the instructor whose courses are to be retrieved.</param>
        /// <param name="paginationRequest">Pagination parameters (pageNumber, pageSize).</param>
        /// <returns>An IActionResult containing the paginated list of courses or appropriate error responses.</returns>
        [HttpGet("GetAllCoursesGivenByInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Instructor")]
        public async Task<IActionResult> GetAllCoursesByInstructorId(Guid Instructorid, [FromQuery] PaginationRequest paginationRequest)
        {

            var getCourses = await courseServices.GetAllCoursesByInstructor(Instructorid);
            if (getCourses.IsError) return NotFound(new ApiResponce {ErrorMassages = getCourses.FirstError.Description });
            
            return Ok(new ApiResponce { Result = (Pagination<Course>.CreateAsync(getCourses.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }




        /// <summary>
        /// Retrieves all custom courses for Main-SubAdmins and Students, paginated.
        /// </summary>
        /// <param name="paginationRequest">Pagination parameters (pageNumber, pageSize).</param>
        /// <returns>An IActionResult containing the paginated list of custom courses or appropriate error responses.</returns>
        [HttpGet("GetAllCustomCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , Student")]
       
        public async Task<IActionResult> GetAllCustomCoursesToMainSubAdmin([FromQuery] PaginationRequest paginationRequest)
        {

            var GetCustomCourses = await courseServices.GetAllCustomCourses();

            return Ok(new ApiResponce { Result= (Pagination<CustomCourseForRetriveDTO>.CreateAsync(GetCustomCourses, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }




        /// <summary>
        /// Retrieves a custom course by its ID for Main-SubAdmins and Students.
        /// </summary>
        /// <param name="id">The ID of the custom course to retrieve.</param>
        /// <returns>An IActionResult containing the custom course or appropriate error responses.</returns>
        [HttpGet("GetCustomCoursesById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , Student")]
        
        public async Task<IActionResult> GetCustomCourseById(Guid id)
        {

            var GetCustomCourse = await courseServices.GetCustomCoursesById(id);
            if (GetCustomCourse.IsError) return NotFound(new ApiResponce { ErrorMassages = GetCustomCourse.FirstError.Description });
            return Ok(new ApiResponce { Result =GetCustomCourse.Value });

        }



        /// <summary>
        /// Retrieves all courses a student is enrolled in.
        /// </summary>
        [HttpGet("GetAllEnrolledCoursesForAStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Student")]
        public async Task<IActionResult> GetEnrolledCourses(Guid studentid, [FromQuery] PaginationRequest paginationRequest)
        {

            var enrolledCourses = await courseServices.GetAllEnrolledCourses(studentid);
            if (enrolledCourses.IsError) return NotFound(new ApiResponce { ErrorMassages = enrolledCourses.FirstError.Description });
            return Ok( new ApiResponce { Result = 
                (Pagination<StudentCourse>.CreateAsync(enrolledCourses.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result}); 
            
        }





       
    }
    }


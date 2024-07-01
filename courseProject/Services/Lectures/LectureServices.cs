using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Emails;
using courseProject.ServiceErrors;
using ErrorOr;

namespace courseProject.Services.Lectures
{
    public class LectureServices : ILectureServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public LectureServices(IUnitOfWork unitOfWork , IMapper mapper , IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
        }

       

        public async Task<ErrorOr<IReadOnlyList<LecturesForRetriveDTO>>> GetAllLecturesByInstructorId(Guid instructorId)
        {
            var instructorFound = await unitOfWork.UserRepository.getUserByIdAsync(instructorId);
            if (instructorFound == null || instructorFound.role.ToLower() != "instructor") return ErrorInstructor.NotFound;
            
            var GetLectures = await unitOfWork.RequestRepository.GetAllConsultationRequestByInstructorIdAsync(instructorId);
          
            var LecturesMapper = mapper.Map<IReadOnlyList<Consultation>, IReadOnlyList<LecturesForRetriveDTO>>(GetLectures);
            return LecturesMapper.ToErrorOr();
           
        }


        public async Task<ErrorOr<Created>> BookALecture(Guid studentId, DateTime date, string startTime, string endTime, BookALectureDTO bookALecture)
        {

            // Retrieve the student information based on the studentId
            var student = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (student == null) return ErrorStudent.NotFound;

            // Validate the time format of startTime and endTime
            if (!CommonClass.IsValidTimeFormat(startTime) || !CommonClass.IsValidTimeFormat(endTime) )
                return ErrorLectures.InvalidTime;// Return an error if the time format is invalid

            // Convert startTime and endTime from string to TimeSpan
            TimeSpan StartTime = CommonClass.ConvertToTimeSpan(startTime);
            TimeSpan EndTime = CommonClass.ConvertToTimeSpan(endTime);

            // Check if the duration of the lecture is within the acceptable range (30 minutes to 2 hours)
            if ((EndTime - StartTime) > TimeSpan.Parse("02:00") || (EndTime - StartTime) < TimeSpan.Parse("00:30"))
                return ErrorLectures.limitationTime; // Return an error if the duration is not within limits

            // Check if the selected time slot is available
            var CheckTime = await unitOfWork.lecturesRepository.showifSelectedTimeIsAvilable(StartTime, EndTime, date);
            if (CheckTime.Count() == 0) return ErrorInstructor.NoInstructorAvailable;

            // Map the BookALectureDTO to a Consultation entity
            var consultation = mapper.Map<BookALectureDTO, Consultation>(bookALecture);
            consultation.StudentId = studentId;
            consultation.startTime = StartTime;
            consultation.endTime = EndTime;
            consultation.date = date;
            consultation.Duration = EndTime - StartTime;

            // Book the lecture by saving the consultation details
            await unitOfWork.lecturesRepository.BookLectureAsync(consultation);
            await unitOfWork.StudentRepository.saveAsync();

            // Map the Consultation entity to a StudentConsultations entity and save it
            var studentConsulation = mapper.Map<Consultation, StudentConsultations>(consultation);
            await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsulation);
            await unitOfWork.StudentRepository.saveAsync();

            // Retrieve the instructor information and send a booking confirmation email
            var instructor = await unitOfWork.UserRepository.getUserByIdAsync(bookALecture.InstructorId);
            // Send a booking confirmation email to the instructor with the details of the booked lecture
            await emailService.SendEmail(
                instructor.email, // Instructor's email address
                "Lecture Booking Confirmation", // Email subject
                EmailTexts.SendBookingEmailAsync(
                    (student.userName + " " + student.LName), // Student's full name
                    bookALecture.name, // Name of the lecture
                    date, // Date of the lecture
                    StartTime, // Start time of the lecture
                    EndTime, // End time of the lecture
                    instructor.userName + " " + instructor.LName // Instructor's full name
                )
            );
            return Result.Created;// Return a success result indicating the lecture was booked
        }

        public async Task<ErrorOr<Created>> JoinToPublicLecture(Guid StudentId, Guid ConsultaionId)
        {
            var student = await unitOfWork.StudentRepository.getStudentByIdAsync(StudentId);
            if (student == null) return ErrorStudent.NotFound;
            var allConsultations = await unitOfWork.lecturesRepository.GetAllPublicConsultationsAsync();
            if (!allConsultations.Any(x => x.Id == ConsultaionId)) return ErrorLectures.NotFound;
                 
            StudentConsultations studentConsultation = new StudentConsultations();
            studentConsultation.StudentId = StudentId;
            studentConsultation.consultationId = ConsultaionId;
            await unitOfWork.StudentRepository.AddInStudentConsulationAsync(studentConsultation);
            await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<IReadOnlyList<PublicLectureForRetriveDTO>>> GetAllConsultations(Guid studentId)
        {

            // Retrieve student information based on the studentId
            var getstudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getstudent == null) return ErrorStudent.NotFound;

            // Retrieve all public consultations
            var allPublicConsultations = await unitOfWork.lecturesRepository.GetAllPublicConsultations();
            var publicConsulations = allPublicConsultations.DistinctBy(x => x.consultationId).ToList();

            // Retrieve all booked private consultations for the student
            var itsPrivateConsultations = await unitOfWork.lecturesRepository.GetAllBookedPrivateConsultationsAsync(studentId);


            // Map public consultations to PublicLectureForRetriveDTO
            IReadOnlyList<PublicLectureForRetriveDTO>? lectureForRetrive = new List<PublicLectureForRetriveDTO>();
            lectureForRetrive = mapper.Map<IReadOnlyList<StudentConsultations>, IReadOnlyList<PublicLectureForRetriveDTO>>(publicConsulations);
            List<StudentConsultations>? allStudent = null;
            List<UserNameDTO>? allPublicStudents = null;

            // For each public lecture, retrieve all students attending the public consultations and map them to UserNameDTO
            foreach (var lecture in lectureForRetrive)
            {
                if (lecture.type.ToLower() == "public")
                {
                    allStudent = await unitOfWork.StudentRepository.GetAllStudentsInPublicConsulations(lecture.consultationId);

                    allPublicStudents = mapper.Map<List<StudentConsultations>, List<UserNameDTO>>(allStudent);
                    lecture.Students = allPublicStudents;
                }

            }

            // Map private consultations to PublicLectureForRetriveDTO
            var privateLectures = mapper.Map<IReadOnlyList<StudentConsultations>, IReadOnlyList<PublicLectureForRetriveDTO>>(itsPrivateConsultations);

            // For each private lecture, map the student attending the private consultation to UserNameDTO
            foreach (var lecture in privateLectures)
            {
                if (lecture.type.ToLower() == "private")
                {
                    var student = itsPrivateConsultations.Where(x => x.consultationId == lecture.consultationId).FirstOrDefault();
                    var studentmapper = mapper.Map<StudentConsultations, UserNameDTO>(student);
                    if (lecture.Students == null)
                    {
                        lecture.Students = new List<UserNameDTO>();
                    }
                    lecture.Students.Add(studentmapper);
                }
            }

            // Combine public and private lectures into a single list
            lectureForRetrive = lectureForRetrive.Concat(privateLectures).ToList();

            // Return the list of lectures as an ErrorOr<IReadOnlyList<PublicLectureForRetriveDTO>>
            return lectureForRetrive.ToErrorOr();
        }

        public async Task<ErrorOr<LecturesForRetriveDTO>> GetConsultationById(Guid consultationId)
        {
            var getConsultation = await unitOfWork.lecturesRepository.GetConsultationById(consultationId);
            if (getConsultation == null) return ErrorLectures.NotFound;
           
         
                var consultationMapper = mapper.Map<Consultation, LecturesForRetriveDTO>(getConsultation);
               return consultationMapper.ToErrorOr();
        
        }
    }
}

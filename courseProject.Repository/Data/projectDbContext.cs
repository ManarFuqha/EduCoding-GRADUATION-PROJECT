using Microsoft.EntityFrameworkCore;
using courseProject.Core.Models;
using courseProject.core.Models;

namespace courseProject.Repository.Data
{
    public class projectDbContext : DbContext
    {
        public projectDbContext() { }
        public projectDbContext(DbContextOptions<projectDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(
                         new User
                          {
                           UserId = userId,
                           userName = "admin",
                           email = "programming.academy24@gmail.com",
                           password = "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse",
                           role = "admin",
                           IsVerified=true,
                           dateOfAdded= DateTime.Now,
                           });

          
            modelBuilder.Entity<Instructor_Working_Hours>(
                e =>
                {
                    e.HasKey(x => new {x.InstructorId , x.day , x.startTime , x.endTime});
                   
                }
                );
            modelBuilder.Entity<MaterialFiles>(
               mf =>
               {
                   mf.HasKey(x => new { x.materialId, x.pdfUrl });

               }
               );

            modelBuilder.Entity<User>()
       .HasMany(u => u.feedbacks)  // User has many Feedbacks
       .WithOne(f => f.instructor)       // Each Feedback belongs to one User
       .HasForeignKey(f => f.InstructorId);

            //     modelBuilder.Entity<User>()
            // .HasMany(u => u.feedbacks)  // User has many Feedbacks
            // .WithOne(f => f.student)       // Each Feedback belongs to one User
            // .HasForeignKey(f => f.StudentId);

            modelBuilder.Entity<User>()
       .HasMany(u => u.consultations)
       .WithOne(f => f.instructor)
       .HasForeignKey(f => f.InstructorId);

            //     modelBuilder.Entity<User>()
            //.HasMany(u => u.consultations)
            //.WithOne(f => f.student)
            //.HasForeignKey(f => f.StudentId);

            modelBuilder.Entity<User>()
       .HasMany(u => u.courses)
       .WithOne(f => f.instructor)
       .HasForeignKey(f => f.InstructorId);

            //     modelBuilder.Entity<User>()
            // .HasMany(u => u.courses)
            // .WithOne(f => f.subAdmin)
            // .HasForeignKey(f => f.subAdminId);

            //     modelBuilder.Entity<User>()
            //.HasMany(u => u.requests)
            //.WithOne(f => f.subAdmin)
            //.HasForeignKey(f => f.SubAdminId);

            modelBuilder.Entity<User>()
       .HasMany(u => u.requests)
       .WithOne(f => f.student)
       .HasForeignKey(f => f.StudentId);

            modelBuilder.Entity<StudentCourse>(
                e =>
                {
                    e.HasKey(x=> new {x.StudentId , x.courseId});
                }
                );
            modelBuilder.Entity<StudentConsultations>(
               e =>
               {
                   e.HasKey(x => new { x.StudentId, x.consultationId });
               }
               );
            modelBuilder.Entity<Student_Task_Submissions>(
               e =>
               {
                   e.HasKey(x => new { x.StudentId, x.TaskId });
               }
               );
            modelBuilder.Entity<InstructorSkills>(
                e =>
                {
                    e.HasKey(x => new {  x.skillId , x.InstructorId });
                });
            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.StudentId)
                      .IsRequired(false);
            });

            //modelBuilder.Entity<Request>(entity =>
            //{
            //    entity.Property(e => e.SubAdminId)
            //          .IsRequired(false);
            //});

            modelBuilder.Entity<CourseMaterial>(entity =>
            {
                entity.Property(e => e.courseId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.InstructorId)
                      .IsRequired(false);
            });
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.CourseId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<User>()
           .Property(c => c.DateOfBirth)
           .HasColumnType("date");

            modelBuilder.Entity<Event>()
           .Property(c => c.dateOfEvent)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.startDate)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.endDate)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.Deadline)
           .HasColumnType("date");

            modelBuilder.Entity<Consultation>()
           .Property(c => c.date)
           .HasColumnType("date");

            modelBuilder.Entity<Request>()
           .Property(c => c.startDate)
           .HasColumnType("date");

            modelBuilder.Entity<Request>()
           .Property(c => c.endDate)
           .HasColumnType("date");
          
        }

        public DbSet<User> users { get; set; }



        public DbSet<Instructor_Working_Hours> Instructor_Working_Hours { get; set; }
   

        public DbSet<Request> requests { get; set; }

        public DbSet<Course> courses { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<CourseMaterial> courseMaterials { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<Consultation> consultations { get; set; }
      
        public DbSet<Student_Task_Submissions> Student_Task_Submissions { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<StudentConsultations> StudentConsultations { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<InstructorSkills> InstructorSkills { get; set;}
        public DbSet<MaterialFiles> MaterialFiles { get; set; }
        public DbSet<Contact> contacts { get; set; }
    }
}

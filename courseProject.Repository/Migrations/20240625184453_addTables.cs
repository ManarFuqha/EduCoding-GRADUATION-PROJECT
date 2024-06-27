using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    skillDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "consultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    startTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    endTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_consultations_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consultations_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Working_Hours",
                columns: table => new
                {
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    endTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Working_Hours", x => new { x.InstructorId, x.day, x.startTime, x.endTime });
                    table.ForeignKey(
                        name: "FK_Instructor_Working_Hours_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSkills",
                columns: table => new
                {
                    skillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSkills", x => new { x.skillId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_InstructorSkills_Skills_skillId",
                        column: x => x.skillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSkills_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    satus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startDate = table.Column<DateTime>(type: "date", nullable: true),
                    endDate = table.Column<DateTime>(type: "date", nullable: true),
                    SubAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requests_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_requests_users_SubAdminId",
                        column: x => x.SubAdminId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StudentConsultations",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    consultationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentConsultations", x => new { x.StudentId, x.consultationId });
                    table.ForeignKey(
                        name: "FK_StudentConsultations_consultations_consultationId",
                        column: x => x.consultationId,
                        principalTable: "consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentConsultations_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    startDate = table.Column<DateTime>(type: "date", nullable: true),
                    endDate = table.Column<DateTime>(type: "date", nullable: true),
                    dateOfUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline = table.Column<DateTime>(type: "date", nullable: true),
                    limitNumberOfStudnet = table.Column<int>(type: "int", nullable: true),
                    totalHours = table.Column<int>(type: "int", nullable: true),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    requestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courses_requests_requestId",
                        column: x => x.requestId,
                        principalTable: "requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courses_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courses_users_subAdminId",
                        column: x => x.subAdminId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateOfEvent = table.Column<DateTime>(type: "date", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    requestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_requests_requestId",
                        column: x => x.requestId,
                        principalTable: "requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_users_SubAdminId",
                        column: x => x.SubAdminId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courseMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    linkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isHidden = table.Column<bool>(type: "bit", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    courseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    consultationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseMaterials_consultations_consultationId",
                        column: x => x.consultationId,
                        principalTable: "consultations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_courseMaterials_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_courseMaterials_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    range = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feedbacks_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_feedbacks_users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "studentCourses",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    courseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentCourses", x => new { x.StudentId, x.courseId });
                    table.ForeignKey(
                        name: "FK_studentCourses_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentCourses_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MaterialFiles",
                columns: table => new
                {
                    materialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pdfUrl = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFiles", x => new { x.materialId, x.pdfUrl });
                    table.ForeignKey(
                        name: "FK_MaterialFiles_courseMaterials_materialId",
                        column: x => x.materialId,
                        principalTable: "courseMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_Task_Submissions",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pdfUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Task_Submissions", x => new { x.StudentId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_Student_Task_Submissions_courseMaterials_TaskId",
                        column: x => x.TaskId,
                        principalTable: "courseMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Task_Submissions_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("f9587613-8def-42c3-93a9-65a644842a58"), null, null, true, null, null, new DateTime(2024, 6, 25, 21, 44, 52, 885, DateTimeKind.Local).AddTicks(3227), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_consultations_InstructorId",
                table: "consultations",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_consultations_StudentId",
                table: "consultations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_courseMaterials_consultationId",
                table: "courseMaterials",
                column: "consultationId");

            migrationBuilder.CreateIndex(
                name: "IX_courseMaterials_courseId",
                table: "courseMaterials",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_courseMaterials_InstructorId",
                table: "courseMaterials",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_InstructorId",
                table: "courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_requestId",
                table: "courses",
                column: "requestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_subAdminId",
                table: "courses",
                column: "subAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_events_requestId",
                table: "events",
                column: "requestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_SubAdminId",
                table: "events",
                column: "SubAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_CourseId",
                table: "feedbacks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_InstructorId",
                table: "feedbacks",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_StudentId",
                table: "feedbacks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSkills_InstructorId",
                table: "InstructorSkills",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_StudentId",
                table: "requests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_SubAdminId",
                table: "requests",
                column: "SubAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Task_Submissions_TaskId",
                table: "Student_Task_Submissions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentConsultations_consultationId",
                table: "StudentConsultations",
                column: "consultationId");

            migrationBuilder.CreateIndex(
                name: "IX_studentCourses_courseId",
                table: "studentCourses",
                column: "courseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "Instructor_Working_Hours");

            migrationBuilder.DropTable(
                name: "InstructorSkills");

            migrationBuilder.DropTable(
                name: "MaterialFiles");

            migrationBuilder.DropTable(
                name: "Student_Task_Submissions");

            migrationBuilder.DropTable(
                name: "StudentConsultations");

            migrationBuilder.DropTable(
                name: "studentCourses");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "courseMaterials");

            migrationBuilder.DropTable(
                name: "consultations");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

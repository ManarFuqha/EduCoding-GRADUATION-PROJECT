using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editfeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_users_InstructorId",
                table: "feedbacks");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("d00536fe-77d8-4e82-a7ff-33b0fcd384bb"));

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "feedbacks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("703616ae-7629-4ea1-859f-c679b7658024"), null, null, true, null, null, new DateTime(2024, 6, 28, 14, 8, 37, 95, DateTimeKind.Local).AddTicks(757), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_InstructorId",
                table: "feedbacks",
                column: "InstructorId",
                principalTable: "users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_users_InstructorId",
                table: "feedbacks");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("703616ae-7629-4ea1-859f-c679b7658024"));

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "feedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("d00536fe-77d8-4e82-a7ff-33b0fcd384bb"), null, null, true, null, null, new DateTime(2024, 6, 26, 23, 48, 17, 809, DateTimeKind.Local).AddTicks(2237), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_InstructorId",
                table: "feedbacks",
                column: "InstructorId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

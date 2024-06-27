using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editconsultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("2c4aee10-ab0d-40e9-9c14-9980d92c4abe"));

            migrationBuilder.DropColumn(
                name: "status",
                table: "consultations");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("d00536fe-77d8-4e82-a7ff-33b0fcd384bb"), null, null, true, null, null, new DateTime(2024, 6, 26, 23, 48, 17, 809, DateTimeKind.Local).AddTicks(2237), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("d00536fe-77d8-4e82-a7ff-33b0fcd384bb"));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "consultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("2c4aee10-ab0d-40e9-9c14-9980d92c4abe"), null, null, true, null, null, new DateTime(2024, 6, 26, 18, 43, 18, 183, DateTimeKind.Local).AddTicks(5009), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });
        }
    }
}

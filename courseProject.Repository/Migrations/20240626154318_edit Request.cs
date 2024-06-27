using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_requests_requestId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_events_requests_requestId",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_requests_users_SubAdminId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_requests_SubAdminId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_events_requestId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_courses_requestId",
                table: "courses");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("f9587613-8def-42c3-93a9-65a644842a58"));

            migrationBuilder.DropColumn(
                name: "SubAdminId",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "events");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "courses");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("2c4aee10-ab0d-40e9-9c14-9980d92c4abe"), null, null, true, null, null, new DateTime(2024, 6, 26, 18, 43, 18, 183, DateTimeKind.Local).AddTicks(5009), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("2c4aee10-ab0d-40e9-9c14-9980d92c4abe"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubAdminId",
                table: "requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "requestId",
                table: "events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "requestId",
                table: "courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "skillDescription", "userName" },
                values: new object[] { new Guid("f9587613-8def-42c3-93a9-65a644842a58"), null, null, true, null, null, new DateTime(2024, 6, 25, 21, 44, 52, 885, DateTimeKind.Local).AddTicks(3227), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_requests_SubAdminId",
                table: "requests",
                column: "SubAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_events_requestId",
                table: "events",
                column: "requestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_requestId",
                table: "courses",
                column: "requestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_requests_requestId",
                table: "courses",
                column: "requestId",
                principalTable: "requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_requests_requestId",
                table: "events",
                column: "requestId",
                principalTable: "requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_requests_users_SubAdminId",
                table: "requests",
                column: "SubAdminId",
                principalTable: "users",
                principalColumn: "UserId");
        }
    }
}

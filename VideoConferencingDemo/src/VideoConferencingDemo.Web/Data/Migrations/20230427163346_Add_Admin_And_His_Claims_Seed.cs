using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoConferencingDemo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Admin_And_His_Claims_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("95e139bb-6751-4d4b-b14f-12e1597ef982"), 0, "e0072f2c-38e7-4da8-8f9f-9fdc1949fa69", "Admin@gmail.com", false, null, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEFJIs3a7tSdJH9bPoQQgws9S9+h5KK10DZ4Adsyb/IfqBHAAPUidCvxtRsl6V5psxQ==", null, false, "ELIU5QDSOYTRPKLL64KM2XUMVH2Z3BG2", false, "Admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "LinkManagement", "true", new Guid("95e139bb-6751-4d4b-b14f-12e1597ef982") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95e139bb-6751-4d4b-b14f-12e1597ef982"));
        }
    }
}

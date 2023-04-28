using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoConferencingDemo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Field_In_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalGeneratedLinq",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95e139bb-6751-4d4b-b14f-12e1597ef982"),
                column: "TotalGeneratedLinq",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGeneratedLinq",
                table: "AspNetUsers");
        }
    }
}

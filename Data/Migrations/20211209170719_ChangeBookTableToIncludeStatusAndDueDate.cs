using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class ChangeBookTableToIncludeStatusAndDueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberInStock",
                table: "Books");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "NumberInStock",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Samples.REST.API.Migrations
{
    public partial class Test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "WeatherForecasts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "WeatherForecasts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

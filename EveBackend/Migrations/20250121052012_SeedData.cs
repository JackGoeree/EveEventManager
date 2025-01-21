using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EveBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Attendees", "Date", "Description", "Location", "MaxAttendees", "Name" },
                values: new object[,]
                {
                    { 1, "[]", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference about the latest in tech.", "Berlin", 300, "Tech Conference 2025" },
                    { 2, "[]", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A local meetup for the community.", "Potsdam", 100, "Community Meetup" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

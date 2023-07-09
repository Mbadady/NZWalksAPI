using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDifficultiesModified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                column: "Name",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                column: "Name",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                column: "Name",
                value: "Hard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                column: "Name",
                value: "Eazy");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                column: "Name",
                value: "");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                column: "Name",
                value: "");
        }
    }
}

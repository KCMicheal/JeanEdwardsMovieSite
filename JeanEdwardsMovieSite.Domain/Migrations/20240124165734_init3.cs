using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeanEdwardsMovieSite.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchQuery",
                table: "SearchQuery");

            migrationBuilder.RenameTable(
                name: "SearchQuery",
                newName: "SearchQueries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchQueries",
                table: "SearchQueries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchQueries",
                table: "SearchQueries");

            migrationBuilder.RenameTable(
                name: "SearchQueries",
                newName: "SearchQuery");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchQuery",
                table: "SearchQuery",
                column: "Id");
        }
    }
}

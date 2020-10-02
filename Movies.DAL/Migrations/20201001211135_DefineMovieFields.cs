using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DAL.Migrations
{
    public partial class DefineMovieFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plot",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Runtime",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Plot",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movies");
        }
    }
}

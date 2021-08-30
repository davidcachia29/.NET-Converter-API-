using Microsoft.EntityFrameworkCore.Migrations;

namespace MyConvertor.Data.Migrations
{
    public partial class SeedMusicsAndArtistsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder
                .Sql("INSERT INTO Convertor(timestamp,success)VALUES ('9876', 1);");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Convertor");
        }
    }
}

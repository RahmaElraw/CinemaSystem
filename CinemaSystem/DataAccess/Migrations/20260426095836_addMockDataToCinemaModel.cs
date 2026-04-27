using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addMockDataToCinemaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                        migrationBuilder.Sql("INSERT INTO Cinemas (Name, Location) VALUES  ('Cinema 1', 'Cairo'), ('Cinema 2', 'Giza'), ('Cinema 3', 'Alexandria'), ('Cinema 4', 'Tanta'), ('Cinema 5', 'Mansoura');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cinemas");
        }
    }
}

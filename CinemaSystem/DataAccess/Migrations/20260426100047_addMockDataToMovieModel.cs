using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.DataAccess.Migrations
{
    public partial class addMockDataToMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO Movies (name, description, price, status, dateTime, mainImg, categoryId, cinemaId)
VALUES
('Michael', 'Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum. Proin eu mi. Nulla ac enim...', 281, 'Available', '2027-09-12', 'service-1', 2, 5),

('Camille Claudel 1915', 'Mauris ullamcorper purus sit amet nulla.', 111, 'Available', '2026-06-14', 'service-2', 4, 4),

('Full Body Massage', 'Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae...', 266, 'Available', '2025-12-29', 'service-3', 5, 5),

('Last Kiss, The (Ultimo bacio, L'')', 'Pellentesque ultrices mattis odio. Donec vitae nisi...', 84, 'Available', '2027-06-13', 'service-4', 4, 3),

('Prelude to War (Why We Fight, 1)', 'Morbi quis tortor id nulla ultrices aliquet.', 89, 'Available', '2025-08-10', 'service-5', 6, 1),

('Poolhall Junkies', 'Nullam porttitor lacus at turpis. Donec posuere metus vitae ipsum.', 11, 'Available', '2025-08-16', 'service-6', 3, 2),

('Wild', 'Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien...', 158, 'Available', '2025-06-28', 'service-7', 2, 2),

('Hot Pepper', 'Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.', 82, 'Available', '2027-07-18', 'service-8', 1, 5),

('Titanic', 'Nulla justo. Aliquam quis turpis eget elit sodales scelerisque.', 136, 'Available', '2027-11-12', 'service-1', 1, 3),

('Brussels Business, The', 'Vestibulum ante ipsum primis in faucibus orci luctus...', 184, 'Available', '2027-05-13', 'service-2', 8, 4);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Movies");
        }
    }
}
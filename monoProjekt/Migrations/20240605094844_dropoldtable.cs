using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monoProjekt.Migrations
{
    /// <inheritdoc />
    public partial class dropoldtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "Items",
           columns: table => new
           {
               Id = table.Column<Guid>(nullable: false),
               Name = table.Column<string>(nullable: false),
               Abrv = table.Column<bool>(nullable: false),
               // Uncomment if you need the DueAt column
               // DueAt = table.Column<DateTimeOffset>(nullable: true)
           });
        }
    }
}

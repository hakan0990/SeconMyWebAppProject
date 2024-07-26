using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAppRehber.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TelNo",
                table: "newRehbers",
                type: "int",
                maxLength: 11,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelNo",
                table: "newRehbers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAppRehber.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loginSurname",
                table: "newRehbers",
                newName: "Soyadı");

            migrationBuilder.RenameColumn(
                name: "loginName",
                table: "newRehbers",
                newName: "YeniKişiAdı");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "newRehbers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "newRehbers");

            migrationBuilder.RenameColumn(
                name: "YeniKişiAdı",
                table: "newRehbers",
                newName: "loginName");

            migrationBuilder.RenameColumn(
                name: "Soyadı",
                table: "newRehbers",
                newName: "loginSurname");
        }
    }
}

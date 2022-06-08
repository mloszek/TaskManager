using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class InitiativeCreatedByIdadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Initiatives",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_CreatedById",
                table: "Initiatives",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_Users_CreatedById",
                table: "Initiatives",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_Users_CreatedById",
                table: "Initiatives");

            migrationBuilder.DropIndex(
                name: "IX_Initiatives_CreatedById",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Initiatives");
        }
    }
}

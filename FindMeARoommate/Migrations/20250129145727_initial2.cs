using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMeARoommate.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Dormitories_DormitoriesId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "DormitoriesId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Dormitories_DormitoriesId",
                table: "Students",
                column: "DormitoriesId",
                principalTable: "Dormitories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Dormitories_DormitoriesId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "DormitoriesId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Dormitories_DormitoriesId",
                table: "Students",
                column: "DormitoriesId",
                principalTable: "Dormitories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

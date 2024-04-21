using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsFakeRepository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserStatements_UserStatementId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecords_AspNetUsers_UserId",
                table: "UserRecords");

            migrationBuilder.DropTable(
                name: "ApplicationUserStatement");

            migrationBuilder.DropIndex(
                name: "IX_UserRecords_UserId",
                table: "UserRecords");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserStatementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserApplicationId",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StatementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserRecordId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserStatementId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserApplicationId",
                table: "UserRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatementId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserRecordId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserStatementId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationUserStatement",
                columns: table => new
                {
                    StatementId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserStatement", x => new { x.StatementId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserStatement_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserStatement_Statements_StatementId",
                        column: x => x.StatementId,
                        principalTable: "Statements",
                        principalColumn: "StatementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRecords_UserId",
                table: "UserRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserStatementId",
                table: "AspNetUsers",
                column: "UserStatementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserStatement_UserId",
                table: "ApplicationUserStatement",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserStatements_UserStatementId",
                table: "AspNetUsers",
                column: "UserStatementId",
                principalTable: "UserStatements",
                principalColumn: "UserStatementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecords_AspNetUsers_UserId",
                table: "UserRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

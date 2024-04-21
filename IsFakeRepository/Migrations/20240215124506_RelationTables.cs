using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsFakeRepository.Migrations
{
    /// <inheritdoc />
    public partial class RelationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatements_Feedbacks_FeedbackId",
                table: "UserStatements");

            migrationBuilder.DropIndex(
                name: "IX_UserStatements_FeedbackId",
                table: "UserStatements");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "UserStatements");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserStatements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatements_ApplicationUserId",
                table: "UserStatements",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecords_ApplicationUserId",
                table: "UserRecords",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecords_AspNetUsers_ApplicationUserId",
                table: "UserRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatements_AspNetUsers_ApplicationUserId",
                table: "UserStatements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecords_AspNetUsers_ApplicationUserId",
                table: "UserRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStatements_AspNetUsers_ApplicationUserId",
                table: "UserStatements");

            migrationBuilder.DropIndex(
                name: "IX_UserStatements_ApplicationUserId",
                table: "UserStatements");

            migrationBuilder.DropIndex(
                name: "IX_UserRecords_ApplicationUserId",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserStatements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserRecords");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "UserStatements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatements_FeedbackId",
                table: "UserStatements",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatements_Feedbacks_FeedbackId",
                table: "UserStatements",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

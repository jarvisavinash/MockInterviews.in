using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockInterviews.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateIdToInterviewRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "InterviewRequests");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Topics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "InterviewRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopicId1",
                table: "InterviewRequests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewRequests_TopicId",
                table: "InterviewRequests",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewRequests_TopicId1",
                table: "InterviewRequests",
                column: "TopicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId",
                table: "InterviewRequests",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId1",
                table: "InterviewRequests",
                column: "TopicId1",
                principalTable: "Topics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId",
                table: "InterviewRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId1",
                table: "InterviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_InterviewRequests_TopicId",
                table: "InterviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_InterviewRequests_TopicId1",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "TopicId1",
                table: "InterviewRequests");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "InterviewRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

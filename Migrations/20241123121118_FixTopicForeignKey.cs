using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockInterviews.Migrations
{
    /// <inheritdoc />
    public partial class FixTopicForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId1",
                table: "InterviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_InterviewRequests_TopicId1",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "TopicId1",
                table: "InterviewRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicId1",
                table: "InterviewRequests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewRequests_TopicId1",
                table: "InterviewRequests",
                column: "TopicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewRequests_Topics_TopicId1",
                table: "InterviewRequests",
                column: "TopicId1",
                principalTable: "Topics",
                principalColumn: "Id");
        }
    }
}

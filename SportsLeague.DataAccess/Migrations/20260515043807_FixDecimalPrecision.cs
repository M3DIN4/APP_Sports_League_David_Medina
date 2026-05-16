using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsLeague.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId1",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId2",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentTeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentTeamId1",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentTeamId2",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TournamentTeamId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TournamentTeamId1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TournamentTeamId2",
                table: "Matches");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "TournamentSponsors",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "TournamentSponsors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<int>(
                name: "TournamentTeamId",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentTeamId1",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentTeamId2",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentTeamId",
                table: "Matches",
                column: "TournamentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentTeamId1",
                table: "Matches",
                column: "TournamentTeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentTeamId2",
                table: "Matches",
                column: "TournamentTeamId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId",
                table: "Matches",
                column: "TournamentTeamId",
                principalTable: "TournamentTeams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId1",
                table: "Matches",
                column: "TournamentTeamId1",
                principalTable: "TournamentTeams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentTeams_TournamentTeamId2",
                table: "Matches",
                column: "TournamentTeamId2",
                principalTable: "TournamentTeams",
                principalColumn: "Id");
        }
    }
}

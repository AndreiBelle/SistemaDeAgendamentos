using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAgendamento.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoIndexData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_DataHoraFim",
                table: "Agendamentos",
                column: "DataHoraFim");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_DatahorarioInicio",
                table: "Agendamentos",
                column: "DatahorarioInicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_DataHoraFim",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_DatahorarioInicio",
                table: "Agendamentos");
        }
    }
}

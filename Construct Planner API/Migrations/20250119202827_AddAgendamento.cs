using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Construct_Planner_API.Migrations
{
    /// <inheritdoc />
    public partial class AddAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObraId = table.Column<int>(type: "integer", nullable: false),
                    DataInicioProjeto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInicioFormas = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInicioConcretagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInicioTransporte = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInicioMontagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFimMontagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_ObraId",
                table: "Agendamentos",
                column: "ObraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos");
        }
    }
}

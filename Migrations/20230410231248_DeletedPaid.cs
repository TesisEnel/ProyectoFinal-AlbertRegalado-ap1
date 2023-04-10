using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class DeletedPaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "PagoId",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "PagoId", "Metodo" },
                values: new object[] { 2, "Tarjeta de credito" });
        }
    }
}

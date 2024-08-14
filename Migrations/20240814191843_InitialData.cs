using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectoef.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("34dfb625-4364-4454-ace0-c5459e33da22"), null, "Actividades deportivas", 25 },
                    { new Guid("42d74f6a-924d-4ba6-bc72-2f7c31f71e4c"), null, "Actividades pendientes", 20 },
                    { new Guid("439eac85-642a-499a-aeae-5624672135d7"), null, "Actividades recreativas", 15 },
                    { new Guid("9fb7df3f-041c-4b46-8d5b-4e4af127c870"), null, "Actividades personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("068df6fd-5b6e-4622-b85e-a657db87e15b"), new Guid("42d74f6a-924d-4ba6-bc72-2f7c31f71e4c"), null, new DateTime(2024, 8, 14, 13, 18, 43, 848, DateTimeKind.Local).AddTicks(3730), 0, "Ver serie shogun" });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("efcca2b4-ad72-46fa-821d-f46997fdbdc5"), new Guid("9fb7df3f-041c-4b46-8d5b-4e4af127c870"), null, new DateTime(2024, 8, 14, 13, 18, 43, 848, DateTimeKind.Local).AddTicks(3690), 1, "Pago servicios publicos" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("34dfb625-4364-4454-ace0-c5459e33da22"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("439eac85-642a-499a-aeae-5624672135d7"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("068df6fd-5b6e-4622-b85e-a657db87e15b"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("efcca2b4-ad72-46fa-821d-f46997fdbdc5"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("42d74f6a-924d-4ba6-bc72-2f7c31f71e4c"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("9fb7df3f-041c-4b46-8d5b-4e4af127c870"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIAutores.Migrations
{
    /// <inheritdoc />
    public partial class DataSetChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autor_AutorId",
                table: "Libros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libros",
                table: "Libros");

            migrationBuilder.RenameTable(
                name: "Libros",
                newName: "Libro");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_AutorId",
                table: "Libro",
                newName: "IX_Libro_AutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libro",
                table: "Libro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Libro_Autor_AutorId",
                table: "Libro",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libro_Autor_AutorId",
                table: "Libro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libro",
                table: "Libro");

            migrationBuilder.RenameTable(
                name: "Libro",
                newName: "Libros");

            migrationBuilder.RenameIndex(
                name: "IX_Libro_AutorId",
                table: "Libros",
                newName: "IX_Libros_AutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libros",
                table: "Libros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autor_AutorId",
                table: "Libros",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
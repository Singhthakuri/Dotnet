using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApp.Migrations
{
    /// <inheritdoc />
    public partial class dbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Patients_PatientId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "AddressTable");

            migrationBuilder.RenameIndex(
                name: "IX_Address_PatientId",
                table: "AddressTable",
                newName: "IX_AddressTable_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressTable",
                table: "AddressTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressTable_Patients_PatientId",
                table: "AddressTable",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressTable_Patients_PatientId",
                table: "AddressTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressTable",
                table: "AddressTable");

            migrationBuilder.RenameTable(
                name: "AddressTable",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_AddressTable_PatientId",
                table: "Address",
                newName: "IX_Address_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Patients_PatientId",
                table: "Address",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SCELIK");

            migrationBuilder.CreateTable(
                name: "YAZAR",
                schema: "SCELIK",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    YAZARADI = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    AKTIFMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    SILINDIMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    EKLEMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PASIFEALMAZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    SILMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("YAZAR_PK", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KITAP",
                schema: "SCELIK",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    KITAPADI = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    YAZARID = table.Column<string>(type: "NVARCHAR2(60)", nullable: false),
                    AKTIFMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    SILINDIMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    EKLEMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PASIFEALMAZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    SILMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("KITAP_PK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KITAP_YAZAR_YAZARID",
                        column: x => x.YAZARID,
                        principalSchema: "SCELIK",
                        principalTable: "YAZAR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KITAPICERIK",
                schema: "SCELIK",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    KITAPID = table.Column<string>(type: "NVARCHAR2(60)", nullable: false),
                    YAZARID = table.Column<string>(type: "NVARCHAR2(60)", nullable: false),
                    BASLIK = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DOSYA = table.Column<string>(type: "CLOB", nullable: true),
                    AKTIFMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    SILINDIMI = table.Column<short>(type: "NUMBER(5)", nullable: false),
                    EKLEMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PASIFEALMAZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    SILMEZAMANI = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("KTP_ICRK_PK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KITAPICERIK_KITAP_KITAPID",
                        column: x => x.KITAPID,
                        principalSchema: "SCELIK",
                        principalTable: "KITAP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KITAPICERIK_YAZAR_YAZARID",
                        column: x => x.YAZARID,
                        principalSchema: "SCELIK",
                        principalTable: "YAZAR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KITAP_YAZARID",
                schema: "SCELIK",
                table: "KITAP",
                column: "YAZARID");

            migrationBuilder.CreateIndex(
                name: "IX_KITAPICERIK_KITAPID",
                schema: "SCELIK",
                table: "KITAPICERIK",
                column: "KITAPID");

            migrationBuilder.CreateIndex(
                name: "IX_KITAPICERIK_YAZARID",
                schema: "SCELIK",
                table: "KITAPICERIK",
                column: "YAZARID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KITAPICERIK",
                schema: "SCELIK");

            migrationBuilder.DropTable(
                name: "KITAP",
                schema: "SCELIK");

            migrationBuilder.DropTable(
                name: "YAZAR",
                schema: "SCELIK");
        }
    }
}

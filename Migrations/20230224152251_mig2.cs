using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPUStoreMVC.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    GPUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Chip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Bus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Memory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GPUImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.GPUID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GPUs");
        }
    }
}

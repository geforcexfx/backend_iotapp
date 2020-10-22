using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Iot_app.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bME680s",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bmetemperature = table.Column<float>(nullable: false),
                    pressure = table.Column<float>(nullable: false),
                    gas = table.Column<float>(nullable: false),
                    humidity = table.Column<float>(nullable: false),
                    casio = table.Column<float>(nullable: false),
                    noaa = table.Column<float>(nullable: false),
                    wiki = table.Column<float>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bME680s", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dHT22s",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    temperature = table.Column<float>(nullable: false),
                    heat_index = table.Column<float>(nullable: false),
                    dew_point = table.Column<float>(nullable: false),
                    humidity = table.Column<float>(nullable: false),
                    comfort = table.Column<string>(nullable: true),
                    light = table.Column<float>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dHT22s", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bME680s");

            migrationBuilder.DropTable(
                name: "dHT22s");
        }
    }
}

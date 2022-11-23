using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Repository.Pattern.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.01),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true, defaultValue: "System"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true, defaultValue: "System")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOn", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 22, 19, 13, 45, 503, DateTimeKind.Utc).AddTicks(1583), "Product One", 1.5 },
                    { 2, new DateTime(2022, 11, 22, 19, 13, 45, 503, DateTimeKind.Utc).AddTicks(1585), "Product Two", 2.5 },
                    { 3, new DateTime(2022, 6, 25, 19, 13, 45, 503, DateTimeKind.Utc).AddTicks(1585), "Old Product", 3.5499999999999998 },
                    { 4, new DateTime(2022, 11, 22, 19, 13, 45, 503, DateTimeKind.Utc).AddTicks(1597), "Expensive Product", 150.99000000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

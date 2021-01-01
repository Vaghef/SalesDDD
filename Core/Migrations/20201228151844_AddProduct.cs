using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodcuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ProductImage = table.Column<byte[]>(nullable: true),
                    BuyPrice = table.Column<double>(nullable: false),
                    SellPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodcuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodcuts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prodcuts_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prodcuts_BrandId",
                table: "Prodcuts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodcuts_UnitId",
                table: "Prodcuts",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prodcuts");
        }
    }
}

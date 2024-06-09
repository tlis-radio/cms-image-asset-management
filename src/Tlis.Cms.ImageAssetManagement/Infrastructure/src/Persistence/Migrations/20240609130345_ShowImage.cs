using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShowImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "show_image",
                schema: "cms_image_asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    show_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_show_image_image_id",
                        column: x => x.id,
                        principalSchema: "cms_image_asset_management",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_show_image_show_id",
                schema: "cms_image_asset_management",
                table: "show_image",
                column: "show_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "show_image",
                schema: "cms_image_asset_management");
        }
    }
}

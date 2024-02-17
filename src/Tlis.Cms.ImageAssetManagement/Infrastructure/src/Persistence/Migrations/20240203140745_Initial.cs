using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms_image_asset_management");

            migrationBuilder.CreateTable(
                name: "image",
                schema: "cms_image_asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_profile_image",
                schema: "cms_image_asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_profile_image_image_id",
                        column: x => x.id,
                        principalSchema: "cms_image_asset_management",
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_profile_image_user_id",
                schema: "cms_image_asset_management",
                table: "user_profile_image",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_profile_image",
                schema: "cms_image_asset_management");

            migrationBuilder.DropTable(
                name: "image",
                schema: "cms_image_asset_management");
        }
    }
}

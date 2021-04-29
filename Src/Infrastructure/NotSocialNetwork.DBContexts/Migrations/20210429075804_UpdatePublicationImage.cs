using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotSocialNetwork.DBContexts.Migrations
{
    public partial class UpdatePublicationImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationImages");

            migrationBuilder.CreateTable(
                name: "ImageEntityPublicationEntity",
                columns: table => new
                {
                    ImagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEntityPublicationEntity", x => new { x.ImagesId, x.PublicationsId });
                    table.ForeignKey(
                        name: "FK_ImageEntityPublicationEntity_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageEntityPublicationEntity_Publications_PublicationsId",
                        column: x => x.PublicationsId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageEntityPublicationEntity_PublicationsId",
                table: "ImageEntityPublicationEntity",
                column: "PublicationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageEntityPublicationEntity");

            migrationBuilder.CreateTable(
                name: "PublicationImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfCreate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationImages_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationImages_ImageId",
                table: "PublicationImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationImages_PublicationId",
                table: "PublicationImages",
                column: "PublicationId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotSocialNetwork.DBContexts.Migrations
{
    public partial class PublicationImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Publications_PublicationEntityId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PublicationEntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PublicationEntityId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "PublicationImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfCreate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationImages");

            migrationBuilder.AddColumn<Guid>(
                name: "PublicationEntityId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PublicationEntityId",
                table: "Images",
                column: "PublicationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Publications_PublicationEntityId",
                table: "Images",
                column: "PublicationEntityId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

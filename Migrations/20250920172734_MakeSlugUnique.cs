using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim_Forum.Migrations
{
    public partial class MakeSlugUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. rendre la colonne non nullable
            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            // 2. créer l'index UNIQUE
            migrationBuilder.CreateIndex(
                name: "IX_categories_slug",
                table: "categories",
                column: "slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Supprimer l'index unique
            migrationBuilder.DropIndex(
                name: "IX_categories_slug",
                table: "categories");

            // Rendre la colonne nullable à nouveau
            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

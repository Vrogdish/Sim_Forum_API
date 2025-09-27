using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim_Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "categories",
                type: "text",
                nullable: true
                );

        }
    }
  

    
}

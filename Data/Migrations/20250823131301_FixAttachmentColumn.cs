using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim_Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAttachmentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Attachments",
                newName: "file_url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_url",
                table: "Attachments",
                newName: "Url");
        }
    }
}

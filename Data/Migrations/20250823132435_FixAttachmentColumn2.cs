using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim_Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAttachmentColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Posts_PostId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Users_UserId",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "threads");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "tags");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "attachments");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "post_tags");

            migrationBuilder.RenameTable(
                name: "PostLikes",
                newName: "post_likes");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "users",
                newName: "avatar_url");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "threads",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "threads",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "threads",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "threads",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "threads",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserId",
                table: "threads",
                newName: "i_x_threads_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_CategoryId",
                table: "threads",
                newName: "i_x_threads_category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tags",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tags",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "posts",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "posts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "posts",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "posts",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "posts",
                newName: "thread_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "posts",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "posts",
                newName: "i_x_posts_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ThreadId",
                table: "posts",
                newName: "i_x_posts_thread_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "attachments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "attachments",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "attachments",
                newName: "file_name");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_PostId",
                table: "attachments",
                newName: "i_x_attachments_post_id");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "post_tags",
                newName: "tag_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "post_tags",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagId",
                table: "post_tags",
                newName: "i_x_post_tags_tag_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "post_likes",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "post_likes",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_UserId",
                table: "post_likes",
                newName: "i_x_post_likes_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PostId_UserId",
                table: "post_likes",
                newName: "IX_post_likes_post_id_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_threads",
                table: "threads",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_tags",
                table: "tags",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_posts",
                table: "posts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_attachments",
                table: "attachments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_tags",
                table: "post_tags",
                columns: new[] { "post_id", "tag_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_likes",
                table: "post_likes",
                columns: new[] { "post_id", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_attachments_posts_post_id",
                table: "attachments",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_likes_posts_post_id",
                table: "post_likes",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_likes_users_user_id",
                table: "post_likes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tags_posts_post_id",
                table: "post_tags",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tags_tags_tag_id",
                table: "post_tags",
                column: "tag_id",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_threads_thread_id",
                table: "posts",
                column: "thread_id",
                principalTable: "threads",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_user_id",
                table: "posts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_threads_categories_category_id",
                table: "threads",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_threads_users_user_id",
                table: "threads",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attachments_posts_post_id",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_post_likes_posts_post_id",
                table: "post_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_post_likes_users_user_id",
                table: "post_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tags_posts_post_id",
                table: "post_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tags_tags_tag_id",
                table: "post_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_threads_thread_id",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_user_id",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_threads_categories_category_id",
                table: "threads");

            migrationBuilder.DropForeignKey(
                name: "FK_threads_users_user_id",
                table: "threads");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_threads",
                table: "threads");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_tags",
                table: "tags");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_posts",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_attachments",
                table: "attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_tags",
                table: "post_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_likes",
                table: "post_likes");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "threads",
                newName: "Threads");

            migrationBuilder.RenameTable(
                name: "tags",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "attachments",
                newName: "Attachments");

            migrationBuilder.RenameTable(
                name: "post_tags",
                newName: "PostTags");

            migrationBuilder.RenameTable(
                name: "post_likes",
                newName: "PostLikes");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "avatar_url",
                table: "Users",
                newName: "AvatarUrl");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Threads",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Threads",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Threads",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Threads",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Threads",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "i_x_threads_user_id",
                table: "Threads",
                newName: "IX_Threads_UserId");

            migrationBuilder.RenameIndex(
                name: "i_x_threads_category_id",
                table: "Threads",
                newName: "IX_Threads_CategoryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tags",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Posts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Posts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "thread_id",
                table: "Posts",
                newName: "ThreadId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Posts",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "i_x_posts_user_id",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameIndex(
                name: "i_x_posts_thread_id",
                table: "Posts",
                newName: "IX_Posts_ThreadId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Attachments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "Attachments",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "Attachments",
                newName: "FileName");

            migrationBuilder.RenameIndex(
                name: "i_x_attachments_post_id",
                table: "Attachments",
                newName: "IX_Attachments_PostId");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "PostTags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "PostTags",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "i_x_post_tags_tag_id",
                table: "PostTags",
                newName: "IX_PostTags_TagId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "PostLikes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "PostLikes",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_post_likes_post_id_user_id",
                table: "PostLikes",
                newName: "IX_PostLikes_PostId_UserId");

            migrationBuilder.RenameIndex(
                name: "i_x_post_likes_user_id",
                table: "PostLikes",
                newName: "IX_PostLikes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Posts_PostId",
                table: "Attachments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Users_UserId",
                table: "PostLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

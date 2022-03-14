using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fakeLook_dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSorce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X_Position = table.Column<double>(type: "float", nullable: false),
                    Y_Position = table.Column<double>(type: "float", nullable: false),
                    Z_Position = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaggedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaggedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaggedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTaggedPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentTag",
                columns: table => new
                {
                    CommentsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentTag", x => new { x.CommentsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CommentTag_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaggedComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaggedComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaggedComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTaggedComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "GroupName" },
                values: new object[,]
                {
                    { 1, "group 0" },
                    { 2, "group 1" },
                    { 3, "group 2" },
                    { 4, "group 3" },
                    { 5, "group 4" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Content" },
                values: new object[,]
                {
                    { 1, "tag 1" },
                    { 2, "tag 2" },
                    { 3, "tag 3" },
                    { 4, "tag 4" },
                    { 5, "tag 5" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "FirstName", "GroupId", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "some adress", "user 1", null, "user 1", "798817788", "user 1" },
                    { 2, "some adress", "user 2", null, "user 2", "798817788", "user 2" },
                    { 3, "some adress", "user 3", null, "user 3", "798817788", "user 3" },
                    { 4, "some adress", "user 4", null, "user 4", "798817788", "user 4" },
                    { 5, "some adress", "user 5", null, "user 5", "798817788", "user 5" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Date", "Description", "ImageSorce", "UserId", "X_Position", "Y_Position", "Z_Position" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 14, 11, 28, 31, 126, DateTimeKind.Local).AddTicks(2357), "post 1", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 1, 21.855248143563326, 41.636375879737244, 7.7570061764184626 },
                    { 2, new DateTime(2022, 3, 14, 11, 28, 31, 126, DateTimeKind.Local).AddTicks(2404), "post 2", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 2, 0.88380156077019212, 41.991374094154096, 5.1980819305554373 },
                    { 3, new DateTime(2022, 3, 14, 11, 28, 31, 126, DateTimeKind.Local).AddTicks(2407), "post 3", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 3, 28.330209283866122, 15.291275128317638, 35.007562813251006 },
                    { 4, new DateTime(2022, 3, 14, 11, 28, 31, 126, DateTimeKind.Local).AddTicks(2409), "post 4", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 4, 37.667449191324167, 41.154295489121331, 0.66327001814449615 },
                    { 5, new DateTime(2022, 3, 14, 11, 28, 31, 126, DateTimeKind.Local).AddTicks(2411), "post 5", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 5, 37.618096742943202, 45.297358628021868, 4.3391866608220813 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "comment 1", 1, 1 },
                    { 2, "comment 2", 1, 2 },
                    { 3, "comment 3", 1, 3 },
                    { 4, "comment 4", 1, 4 },
                    { 5, "comment 5", 1, 5 },
                    { 6, "comment 1", 2, 1 },
                    { 7, "comment 2", 2, 2 },
                    { 8, "comment 3", 2, 3 },
                    { 9, "comment 4", 2, 4 },
                    { 10, "comment 5", 2, 5 },
                    { 11, "comment 1", 3, 1 },
                    { 12, "comment 2", 3, 2 },
                    { 13, "comment 3", 3, 3 },
                    { 14, "comment 4", 3, 4 },
                    { 15, "comment 5", 3, 5 },
                    { 16, "comment 1", 4, 1 },
                    { 17, "comment 2", 4, 2 },
                    { 18, "comment 3", 4, 3 },
                    { 19, "comment 4", 4, 4 },
                    { 20, "comment 5", 4, 5 },
                    { 21, "comment 1", 5, 1 },
                    { 22, "comment 2", 5, 2 },
                    { 23, "comment 3", 5, 3 },
                    { 24, "comment 4", 5, 4 },
                    { 25, "comment 5", 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "IsActive", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 1, 2 },
                    { 3, true, 1, 3 },
                    { 4, true, 1, 4 },
                    { 5, true, 1, 5 },
                    { 6, true, 2, 1 },
                    { 7, true, 2, 2 },
                    { 8, true, 2, 3 },
                    { 9, true, 2, 4 },
                    { 10, true, 2, 5 },
                    { 11, true, 3, 1 },
                    { 12, true, 3, 2 },
                    { 13, true, 3, 3 },
                    { 14, true, 3, 4 },
                    { 15, true, 3, 5 },
                    { 16, true, 4, 1 },
                    { 17, true, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "IsActive", "PostId", "UserId" },
                values: new object[,]
                {
                    { 18, true, 4, 3 },
                    { 19, true, 4, 4 },
                    { 20, true, 4, 5 },
                    { 21, true, 5, 1 },
                    { 22, true, 5, 2 },
                    { 23, true, 5, 3 },
                    { 24, true, 5, 4 },
                    { 25, true, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "UserTaggedPosts",
                columns: new[] { "Id", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, 5, 1 },
                    { 2, 4, 2 },
                    { 3, 3, 3 },
                    { 4, 2, 4 },
                    { 5, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "UserTaggedComments",
                columns: new[] { "Id", "CommentId", "UserId" },
                values: new object[,]
                {
                    { 1, 23, 5 },
                    { 2, 7, 1 },
                    { 3, 9, 4 },
                    { 4, 17, 2 },
                    { 5, 16, 4 },
                    { 6, 19, 2 },
                    { 7, 17, 1 },
                    { 8, 19, 1 },
                    { 9, 12, 1 },
                    { 10, 7, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTag_TagsId",
                table: "CommentTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupName",
                table: "Group",
                column: "GroupName",
                unique: true,
                filter: "[GroupName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedComments_CommentId",
                table: "UserTaggedComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedComments_UserId",
                table: "UserTaggedComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedPosts_PostId",
                table: "UserTaggedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedPosts_UserId",
                table: "UserTaggedPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentTag");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "UserTaggedComments");

            migrationBuilder.DropTable(
                name: "UserTaggedPosts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}

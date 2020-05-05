using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABPBlog.Migrations
{
    public partial class NewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicNode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ParentId = table.Column<int>(nullable: false),
                    NodeName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicNode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserName = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Profile = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    GitHub = table.Column<string>(nullable: true),
                    TopicCount = table.Column<int>(nullable: false),
                    TopicReplyCount = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false),
                    LastTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    NodeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Top = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    ReplyCount = table.Column<int>(nullable: false),
                    LastReplyUserId = table.Column<string>(nullable: true),
                    LastReplyUserId1 = table.Column<int>(nullable: true),
                    LastReplyTime = table.Column<DateTime>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_User_LastReplyUserId1",
                        column: x => x.LastReplyUserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topic_TopicNode_NodeId",
                        column: x => x.NodeId,
                        principalTable: "TopicNode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topic_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SendUserId = table.Column<string>(nullable: true),
                    SendUserId1 = table.Column<int>(nullable: true),
                    ReceiveUserId = table.Column<string>(nullable: true),
                    ReceiveUserId1 = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessage_User_ReceiveUserId1",
                        column: x => x.ReceiveUserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessage_User_SendUserId1",
                        column: x => x.SendUserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicReply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TopicId = table.Column<int>(nullable: false),
                    ReplyUserId = table.Column<string>(nullable: true),
                    ReplyUserId1 = table.Column<int>(nullable: true),
                    ReplyEmail = table.Column<string>(nullable: true),
                    ReplyContent = table.Column<string>(nullable: true),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicReply_User_ReplyUserId1",
                        column: x => x.ReplyUserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicReply_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCollection_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topic_LastReplyUserId1",
                table: "Topic",
                column: "LastReplyUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_NodeId",
                table: "Topic",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_UserId1",
                table: "Topic",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TopicReply_ReplyUserId1",
                table: "TopicReply",
                column: "ReplyUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TopicReply_TopicId",
                table: "TopicReply",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollection_TopicId",
                table: "UserCollection",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_ReceiveUserId1",
                table: "UserMessage",
                column: "ReceiveUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_SendUserId1",
                table: "UserMessage",
                column: "SendUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicReply");

            migrationBuilder.DropTable(
                name: "UserCollection");

            migrationBuilder.DropTable(
                name: "UserMessage");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TopicNode");
        }
    }
}

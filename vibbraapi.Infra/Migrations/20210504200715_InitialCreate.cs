using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vibbraapi.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PROJECT",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROJECT", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "TB_TIME",
                columns: table => new
                {
                    time_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Id = table.Column<long>(type: "bigint", nullable: false),
                    User_Id = table.Column<long>(type: "bigint", nullable: false),
                    Started_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ended_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TIME", x => new { x.time_id, x.Project_Id, x.User_Id });
                    table.ForeignKey(
                        name: "FK_TB_TIME_TB_PROJECT_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "TB_PROJECT",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TIME_TB_USER_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_USER",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TIME_Project_Id",
                table: "TB_TIME",
                column: "Project_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TIME_User_Id",
                table: "TB_TIME",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TIME");

            migrationBuilder.DropTable(
                name: "TB_PROJECT");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}

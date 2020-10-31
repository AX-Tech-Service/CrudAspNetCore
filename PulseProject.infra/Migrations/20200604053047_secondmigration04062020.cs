using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PulseProject.infra.Migrations
{
    public partial class secondmigration04062020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    inUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    stLastName = table.Column<string>(maxLength: 50, nullable: true),
                    dtBirthDate = table.Column<DateTime>(nullable: false),
                    stEmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    stPassword = table.Column<string>(maxLength: 50, nullable: true),
                    dtCreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.inUserId);
                });

            migrationBuilder.CreateTable(
                name: "UserList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "UserList");
        }
    }
}

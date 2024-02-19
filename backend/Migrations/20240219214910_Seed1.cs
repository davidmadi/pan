using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackApi.Migrations
{
    /// <inheritdoc />
    public partial class Seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Users", new string[]{"Id", "FullName", "Email"}, new string[]{"1", "David Madi", "davidmadi@gmail.com"});
            migrationBuilder.InsertData("Users", new string[]{"Id", "FullName", "Email"}, new string[]{"2", "Ethan Hunt", "ethan@gmail.com"});
            migrationBuilder.InsertData("Users", new string[]{"Id", "FullName", "Email"}, new string[]{"3", "James Bond", "james@gmail.com"});
            migrationBuilder.InsertData("Users", new string[]{"Id", "FullName", "Email"}, new string[]{"4", "Aang Air", "aang@gmail.com"});
            migrationBuilder.InsertData("Users", new string[]{"Id", "FullName", "Email"}, new string[]{"5", "Katara Air", "katara@gmail.com"});
            migrationBuilder.InsertData("Networks", new string[]{"PrimaryId", "FriendId", "Relationship"}, new string[]{"1", "2", "friend"});
            migrationBuilder.InsertData("Networks", new string[]{"PrimaryId", "FriendId", "Relationship"}, new string[]{"1", "3", "friend"});
            migrationBuilder.InsertData("Networks", new string[]{"PrimaryId", "FriendId", "Relationship"}, new string[]{"2", "3", "friend"});
            migrationBuilder.InsertData("Networks", new string[]{"PrimaryId", "FriendId", "Relationship"}, new string[]{"4", "5", "family"});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

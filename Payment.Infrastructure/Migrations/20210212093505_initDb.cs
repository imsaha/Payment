using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Infrastructure.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Card_CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Card_ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Card_SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    At = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPaymentStates_tblPayments_RequestId",
                        column: x => x.RequestId,
                        principalTable: "tblPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentStates_RequestId_State_At",
                table: "tblPaymentStates",
                columns: new[] { "RequestId", "State", "At" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPaymentStates");

            migrationBuilder.DropTable(
                name: "tblPayments");
        }
    }
}

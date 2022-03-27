using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCharging.Entities.Migrations
{
    public partial class RelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargingGroup",
                columns: table => new
                {
                    GroupIdentifier = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(maxLength: 100, nullable: false),
                    CapacityInAmps = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingGroup", x => x.GroupIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "ChargingStation",
                columns: table => new
                {
                    StationIdentifier = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(maxLength: 100, nullable: false),
                    GroupIdentifier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingStation", x => x.StationIdentifier);
                    table.ForeignKey(
                        name: "FK_ChargingStation_ChargingGroup_GroupIdentifier",
                        column: x => x.GroupIdentifier,
                        principalTable: "ChargingGroup",
                        principalColumn: "GroupIdentifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargingConnector",
                columns: table => new
                {
                    ConnectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectorIdentifier = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    MaxCurrent = table.Column<int>(nullable: false),
                    StationIdentifier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingConnector", x => x.ConnectorId);
                    table.ForeignKey(
                        name: "FK_ChargingConnector_ChargingStation_StationIdentifier",
                        column: x => x.StationIdentifier,
                        principalTable: "ChargingStation",
                        principalColumn: "StationIdentifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargingConnector_StationIdentifier",
                table: "ChargingConnector",
                column: "StationIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStation_GroupIdentifier",
                table: "ChargingStation",
                column: "GroupIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingConnector");

            migrationBuilder.DropTable(
                name: "ChargingStation");

            migrationBuilder.DropTable(
                name: "ChargingGroup");
        }
    }
}

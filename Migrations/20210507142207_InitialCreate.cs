using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacjaKurierska.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryWindow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<string>(type: "TEXT", nullable: false),
                    To = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryWindow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseDate = table.Column<string>(type: "TEXT", nullable: true),
                    ModulID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransitTimeAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Monday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tuesday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Wednesday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Thursday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Friday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Saturday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Sunday = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitTimeAvailability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    UserType = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moduls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryWindowId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moduls_DeliveryWindow_DeliveryWindowId",
                        column: x => x.DeliveryWindowId,
                        principalTable: "DeliveryWindow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PredictableDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<string>(type: "TEXT", nullable: false),
                    To = table.Column<string>(type: "TEXT", nullable: false),
                    ResponseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictableDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictableDates_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ModulId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Moduls_ModulId",
                        column: x => x.ModulId,
                        principalTable: "Moduls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransitTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<string>(type: "TEXT", nullable: false),
                    To = table.Column<string>(type: "TEXT", nullable: false),
                    DispatchId = table.Column<int>(type: "INTEGER", nullable: true),
                    TransitId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeliveryId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransitTime_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitTime_TransitTimeAvailability_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "TransitTimeAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitTime_TransitTimeAvailability_DispatchId",
                        column: x => x.DispatchId,
                        principalTable: "TransitTimeAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitTime_TransitTimeAvailability_TransitId",
                        column: x => x.TransitId,
                        principalTable: "TransitTimeAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moduls_DeliveryWindowId",
                table: "Moduls",
                column: "DeliveryWindowId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictableDates_ResponseId",
                table: "PredictableDates",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ModulId",
                table: "Service",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitTime_DeliveryId",
                table: "TransitTime",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitTime_DispatchId",
                table: "TransitTime",
                column: "DispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitTime_ServiceId",
                table: "TransitTime",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitTime_TransitId",
                table: "TransitTime",
                column: "TransitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictableDates");

            migrationBuilder.DropTable(
                name: "TransitTime");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "TransitTimeAvailability");

            migrationBuilder.DropTable(
                name: "Moduls");

            migrationBuilder.DropTable(
                name: "DeliveryWindow");
        }
    }
}

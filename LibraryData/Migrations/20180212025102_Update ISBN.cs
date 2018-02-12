using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LibraryData.Migrations
{
    public partial class UpdateISBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckOut_LibraryAsset_LibraryAssetId",
                table: "CheckOut");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOut_LibraryCard_LibraryCardId",
                table: "CheckOut");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAsset_LibraryBranch_LocationId",
                table: "LibraryAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAsset_Status_StatusId",
                table: "LibraryAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_Patron_LibraryBranch_HomeLibraryBranchId",
                table: "Patron");

            migrationBuilder.DropForeignKey(
                name: "FK_Patron_LibraryCard_LibraryCardId",
                table: "Patron");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patron",
                table: "Patron");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryCard",
                table: "LibraryCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryBranch",
                table: "LibraryBranch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryAsset",
                table: "LibraryAsset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckOut",
                table: "CheckOut");

            migrationBuilder.RenameTable(
                name: "Patron",
                newName: "Patrons");

            migrationBuilder.RenameTable(
                name: "LibraryCard",
                newName: "LibraryCards");

            migrationBuilder.RenameTable(
                name: "LibraryBranch",
                newName: "LibraryBranchs");

            migrationBuilder.RenameTable(
                name: "LibraryAsset",
                newName: "LibraryAssets");

            migrationBuilder.RenameTable(
                name: "CheckOut",
                newName: "CheckOuts");

            migrationBuilder.RenameIndex(
                name: "IX_Patron_LibraryCardId",
                table: "Patrons",
                newName: "IX_Patrons_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Patron_HomeLibraryBranchId",
                table: "Patrons",
                newName: "IX_Patrons_HomeLibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAsset_StatusId",
                table: "LibraryAssets",
                newName: "IX_LibraryAssets_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAsset_LocationId",
                table: "LibraryAssets",
                newName: "IX_LibraryAssets_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOut_LibraryCardId",
                table: "CheckOuts",
                newName: "IX_CheckOuts_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOut_LibraryAssetId",
                table: "CheckOuts",
                newName: "IX_CheckOuts_LibraryAssetId");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "LibraryAssets",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patrons",
                table: "Patrons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryCards",
                table: "LibraryCards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryBranchs",
                table: "LibraryBranchs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryAssets",
                table: "LibraryAssets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckOuts",
                table: "CheckOuts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BranchHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: true),
                    CloseTime = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OpenTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchHours_LibraryBranchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "LibraryBranchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckIn = table.Column<DateTime>(nullable: false),
                    CheckOut = table.Column<DateTime>(nullable: false),
                    LibraryAssetId = table.Column<int>(nullable: false),
                    LibraryCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutHistorys_LibraryAssets_LibraryAssetId",
                        column: x => x.LibraryAssetId,
                        principalTable: "LibraryAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutHistorys_LibraryCards_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HoldPlaced = table.Column<DateTime>(nullable: false),
                    LibraryAssetId = table.Column<int>(nullable: true),
                    LibraryCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holds_LibraryAssets_LibraryAssetId",
                        column: x => x.LibraryAssetId,
                        principalTable: "LibraryAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holds_LibraryCards_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchHours_BranchId",
                table: "BranchHours",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutHistorys_LibraryAssetId",
                table: "CheckoutHistorys",
                column: "LibraryAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutHistorys_LibraryCardId",
                table: "CheckoutHistorys",
                column: "LibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_LibraryCardId",
                table: "Holds",
                column: "LibraryCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_LibraryAssets_LibraryAssetId",
                table: "CheckOuts",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_LibraryCards_LibraryCardId",
                table: "CheckOuts",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_LibraryBranchs_LocationId",
                table: "LibraryAssets",
                column: "LocationId",
                principalTable: "LibraryBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_Status_StatusId",
                table: "LibraryAssets",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryBranchs_HomeLibraryBranchId",
                table: "Patrons",
                column: "HomeLibraryBranchId",
                principalTable: "LibraryBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryCards_LibraryCardId",
                table: "Patrons",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_LibraryAssets_LibraryAssetId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_LibraryCards_LibraryCardId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_LibraryBranchs_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_Status_StatusId",
                table: "LibraryAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryBranchs_HomeLibraryBranchId",
                table: "Patrons");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryCards_LibraryCardId",
                table: "Patrons");

            migrationBuilder.DropTable(
                name: "BranchHours");

            migrationBuilder.DropTable(
                name: "CheckoutHistorys");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patrons",
                table: "Patrons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryCards",
                table: "LibraryCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryBranchs",
                table: "LibraryBranchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryAssets",
                table: "LibraryAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckOuts",
                table: "CheckOuts");

            migrationBuilder.RenameTable(
                name: "Patrons",
                newName: "Patron");

            migrationBuilder.RenameTable(
                name: "LibraryCards",
                newName: "LibraryCard");

            migrationBuilder.RenameTable(
                name: "LibraryBranchs",
                newName: "LibraryBranch");

            migrationBuilder.RenameTable(
                name: "LibraryAssets",
                newName: "LibraryAsset");

            migrationBuilder.RenameTable(
                name: "CheckOuts",
                newName: "CheckOut");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_LibraryCardId",
                table: "Patron",
                newName: "IX_Patron_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_HomeLibraryBranchId",
                table: "Patron",
                newName: "IX_Patron_HomeLibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAssets_StatusId",
                table: "LibraryAsset",
                newName: "IX_LibraryAsset_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAssets_LocationId",
                table: "LibraryAsset",
                newName: "IX_LibraryAsset_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOuts_LibraryCardId",
                table: "CheckOut",
                newName: "IX_CheckOut_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOuts_LibraryAssetId",
                table: "CheckOut",
                newName: "IX_CheckOut_LibraryAssetId");

            migrationBuilder.AlterColumn<int>(
                name: "ISBN",
                table: "LibraryAsset",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patron",
                table: "Patron",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryCard",
                table: "LibraryCard",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryBranch",
                table: "LibraryBranch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryAsset",
                table: "LibraryAsset",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckOut",
                table: "CheckOut",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOut_LibraryAsset_LibraryAssetId",
                table: "CheckOut",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOut_LibraryCard_LibraryCardId",
                table: "CheckOut",
                column: "LibraryCardId",
                principalTable: "LibraryCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAsset_LibraryBranch_LocationId",
                table: "LibraryAsset",
                column: "LocationId",
                principalTable: "LibraryBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAsset_Status_StatusId",
                table: "LibraryAsset",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patron_LibraryBranch_HomeLibraryBranchId",
                table: "Patron",
                column: "HomeLibraryBranchId",
                principalTable: "LibraryBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patron_LibraryCard_LibraryCardId",
                table: "Patron",
                column: "LibraryCardId",
                principalTable: "LibraryCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

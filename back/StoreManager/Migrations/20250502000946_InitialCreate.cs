using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StoreManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "BusinessPartners",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    StreetNumber = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPartners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    FileName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MechanicalComponents",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Identifier = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CurrentStock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicalComponents", x => x.Id);
                    table.UniqueConstraint("AK_MechanicalComponents_Identifier", x => x.Identifier);
                    table.CheckConstraint("CK_MECHANICAL_COMPONENT_STOCK_NONNEGATIVE", "\"CurrentStock\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "ProductBlueprints",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Identifier = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBlueprints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentChunks",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChunkNumber = table.Column<int>(type: "integer", nullable: false),
                    SupaBasePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentChunks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentChunks_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "public",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateIssued = table.Column<DateOnly>(type: "date", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "public",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBatches",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlueprintId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBatches_ProductBlueprints_BlueprintId",
                        column: x => x.BlueprintId,
                        principalSchema: "public",
                        principalTable: "ProductBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductComponents",
                schema: "public",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsedQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComponents", x => new { x.ProductId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_ProductComponents_MechanicalComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "public",
                        principalTable: "MechanicalComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductComponents_ProductBlueprints_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "public",
                        principalTable: "ProductBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exports",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExporterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exports_BusinessPartners_ExporterId",
                        column: x => x.ExporterId,
                        principalSchema: "public",
                        principalTable: "BusinessPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exports_Invoices_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imports",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imports_BusinessPartners_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "public",
                        principalTable: "BusinessPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Imports_BusinessPartners_ProviderId1",
                        column: x => x.ProviderId1,
                        principalSchema: "public",
                        principalTable: "BusinessPartners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Imports_Invoices_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportItems",
                schema: "public",
                columns: table => new
                {
                    ExportId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PricePerPiece = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportItems", x => new { x.ExportId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ExportItems_Exports_ExportId",
                        column: x => x.ExportId,
                        principalSchema: "public",
                        principalTable: "Exports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportItems_ProductBlueprints_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "public",
                        principalTable: "ProductBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportItems",
                schema: "public",
                columns: table => new
                {
                    ImportId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PricePerPiece = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportItems", x => new { x.ImportId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_ImportItems_Imports_ImportId",
                        column: x => x.ImportId,
                        principalSchema: "public",
                        principalTable: "Imports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportItems_MechanicalComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "public",
                        principalTable: "MechanicalComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_DocumentId",
                schema: "public",
                table: "DocumentChunks",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_SupaBasePath",
                schema: "public",
                table: "DocumentChunks",
                column: "SupaBasePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FileName",
                schema: "public",
                table: "Documents",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportItems_ProductId",
                schema: "public",
                table: "ExportItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Exports_ExporterId",
                schema: "public",
                table: "Exports",
                column: "ExporterId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_ComponentId",
                schema: "public",
                table: "ImportItems",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Imports_ProviderId",
                schema: "public",
                table: "Imports",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Imports_ProviderId1",
                schema: "public",
                table: "Imports",
                column: "ProviderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DocumentId",
                schema: "public",
                table: "Invoices",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatches_BlueprintId",
                schema: "public",
                table: "ProductBatches",
                column: "BlueprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBlueprints_Identifier",
                schema: "public",
                table: "ProductBlueprints",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponents_ComponentId",
                schema: "public",
                table: "ProductComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                schema: "public",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "public",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "public",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentChunks",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ExportItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ImportItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductBatches",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductComponents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Exports",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Imports",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MechanicalComponents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductBlueprints",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BusinessPartners",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "public");
        }
    }
}

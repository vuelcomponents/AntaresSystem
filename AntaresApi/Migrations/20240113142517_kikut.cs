using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntaresApi.Migrations
{
    /// <inheritdoc />
    public partial class kikut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comparators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparators", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PositionUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionUnits", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RecruitmentContacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Done = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Link = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentContacts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatusActionTriggers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusActionTriggers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StoreModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreModels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SystemFunctions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemFunctions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VariantTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Vin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Produced = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Bought = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    TextData1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextData2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextData3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextData4 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true),
                    IntData1 = table.Column<int>(type: "int", nullable: true),
                    IntData2 = table.Column<int>(type: "int", nullable: true),
                    DoubleData1 = table.Column<double>(type: "double", nullable: true),
                    DoubleData2 = table.Column<double>(type: "double", nullable: true),
                    DateData1 = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateData2 = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    HouseAndLocalNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreetName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reserved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Statuses_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StoreModelSystemFunction",
                columns: table => new
                {
                    StoreModelsId = table.Column<long>(type: "bigint", nullable: false),
                    SystemFunctionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreModelSystemFunction", x => new { x.StoreModelsId, x.SystemFunctionsId });
                    table.ForeignKey(
                        name: "FK_StoreModelSystemFunction_StoreModels_StoreModelsId",
                        column: x => x.StoreModelsId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreModelSystemFunction_SystemFunctions_SystemFunctionsId",
                        column: x => x.SystemFunctionsId,
                        principalTable: "SystemFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarHouse",
                columns: table => new
                {
                    CarsId = table.Column<long>(type: "bigint", nullable: false),
                    HousesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarHouse", x => new { x.CarsId, x.HousesId });
                    table.ForeignKey(
                        name: "FK_CarHouse_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarHouse_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseAndLocalNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreetName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileData = table.Column<byte[]>(type: "longblob", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VariantTypeId = table.Column<long>(type: "bigint", nullable: false),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: true),
                    Global = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Variants_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Variants_VariantTypes_VariantTypeId",
                        column: x => x.VariantTypeId,
                        principalTable: "VariantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarCompany",
                columns: table => new
                {
                    CarsId = table.Column<long>(type: "bigint", nullable: false),
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompany", x => new { x.CarsId, x.CompaniesId });
                    table.ForeignKey(
                        name: "FK_CarCompany_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarCompany_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompanyHouse",
                columns: table => new
                {
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false),
                    HousesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHouse", x => new { x.CompaniesId, x.HousesId });
                    table.ForeignKey(
                        name: "FK_CompanyHouse_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHouse_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Class = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Start = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Stop = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PositionUnitId = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentQty = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Demand = table.Column<double>(type: "double", nullable: true),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_PositionUnits_PositionUnitId",
                        column: x => x.PositionUnitId,
                        principalTable: "PositionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Positions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Car_Document",
                columns: table => new
                {
                    CarsId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Document", x => new { x.CarsId, x.DocumentsId });
                    table.ForeignKey(
                        name: "FK_Car_Document_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Company_Document",
                columns: table => new
                {
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Document", x => new { x.CompaniesId, x.DocumentsId });
                    table.ForeignKey(
                        name: "FK_Company_Document_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "House_Document",
                columns: table => new
                {
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false),
                    HousesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House_Document", x => new { x.DocumentsId, x.HousesId });
                    table.ForeignKey(
                        name: "FK_House_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_House_Document_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mail_Document",
                columns: table => new
                {
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false),
                    MailsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail_Document", x => new { x.DocumentsId, x.MailsId });
                    table.ForeignKey(
                        name: "FK_Mail_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mail_Document_Mails_MailsId",
                        column: x => x.MailsId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VariantRealisations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VariantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantRealisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantRealisations_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActionFunctions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SystemFunctionId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    MailId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionFunctions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionFunctions_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionFunctions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionFunctions_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionFunctions_SystemFunctions_SystemFunctionId",
                        column: x => x.SystemFunctionId,
                        principalTable: "SystemFunctions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Global = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Documents_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Realisations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    VariantRealisationId = table.Column<long>(type: "bigint", nullable: true),
                    NumericValue = table.Column<double>(type: "double", nullable: true),
                    DateValue = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MadeByUser = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ComparatorId = table.Column<long>(type: "bigint", nullable: true),
                    ActionFunctionId = table.Column<long>(type: "bigint", nullable: true),
                    HouseId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Realisations_ActionFunctions_ActionFunctionId",
                        column: x => x.ActionFunctionId,
                        principalTable: "ActionFunctions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Realisations_Comparators_ComparatorId",
                        column: x => x.ComparatorId,
                        principalTable: "Comparators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Realisations_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Realisations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Realisations_VariantRealisations_VariantRealisationId",
                        column: x => x.VariantRealisationId,
                        principalTable: "VariantRealisations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Realisations_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatusActions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionFunctionId = table.Column<long>(type: "bigint", nullable: false),
                    StatusActionTriggerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusActions_ActionFunctions_ActionFunctionId",
                        column: x => x.ActionFunctionId,
                        principalTable: "ActionFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusActions_StatusActionTriggers_StatusActionTriggerId",
                        column: x => x.StatusActionTriggerId,
                        principalTable: "StatusActionTriggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recruitments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfferId = table.Column<long>(type: "bigint", nullable: true),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    Open = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruitments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recruitments_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recruitments_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruitments_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarRealisation",
                columns: table => new
                {
                    CarsId = table.Column<long>(type: "bigint", nullable: false),
                    RealisationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRealisation", x => new { x.CarsId, x.RealisationsId });
                    table.ForeignKey(
                        name: "FK_CarRealisation_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRealisation_Realisations_RealisationsId",
                        column: x => x.RealisationsId,
                        principalTable: "Realisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompanyRealisation",
                columns: table => new
                {
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false),
                    RealisationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRealisation", x => new { x.CompaniesId, x.RealisationsId });
                    table.ForeignKey(
                        name: "FK_CompanyRealisation_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRealisation_Realisations_RealisationsId",
                        column: x => x.RealisationsId,
                        principalTable: "Realisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Realisation_Document",
                columns: table => new
                {
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false),
                    RealisationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realisation_Document", x => new { x.DocumentsId, x.RealisationsId });
                    table.ForeignKey(
                        name: "FK_Realisation_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Realisation_Document_Realisations_RealisationsId",
                        column: x => x.RealisationsId,
                        principalTable: "Realisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatusStatusAction",
                columns: table => new
                {
                    StatusActionsId = table.Column<long>(type: "bigint", nullable: false),
                    StatusesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusStatusAction", x => new { x.StatusActionsId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_StatusStatusAction_StatusActions_StatusActionsId",
                        column: x => x.StatusActionsId,
                        principalTable: "StatusActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusStatusAction_Statuses_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Verified = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    VerifyToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PrivatePhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseAndLocalNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreetName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubHouseAndLocalNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubStreetName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubPostcode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubCity = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreModelId = table.Column<long>(type: "bigint", nullable: false),
                    Bsn = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pesel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    PreviousStatusId = table.Column<long>(type: "bigint", nullable: true),
                    RecruitmentId = table.Column<long>(type: "bigint", nullable: true),
                    RecruitmentContactId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_RecruitmentContacts_RecruitmentContactId",
                        column: x => x.RecruitmentContactId,
                        principalTable: "RecruitmentContacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Recruitments_RecruitmentId",
                        column: x => x.RecruitmentId,
                        principalTable: "Recruitments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Statuses_PreviousStatusId",
                        column: x => x.PreviousStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_StoreModels_StoreModelId",
                        column: x => x.StoreModelId,
                        principalTable: "StoreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recruitment_DocumentType",
                columns: table => new
                {
                    DocumentTypesId = table.Column<long>(type: "bigint", nullable: false),
                    RecruitmentsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment_DocumentType", x => new { x.DocumentTypesId, x.RecruitmentsId });
                    table.ForeignKey(
                        name: "FK_Recruitment_DocumentType_DocumentTypes_DocumentTypesId",
                        column: x => x.DocumentTypesId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruitment_DocumentType_Recruitments_RecruitmentsId",
                        column: x => x.RecruitmentsId,
                        principalTable: "Recruitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RecruitmentVariant",
                columns: table => new
                {
                    RecruitmentsId = table.Column<long>(type: "bigint", nullable: false),
                    VariantsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentVariant", x => new { x.RecruitmentsId, x.VariantsId });
                    table.ForeignKey(
                        name: "FK_RecruitmentVariant_Recruitments_RecruitmentsId",
                        column: x => x.RecruitmentsId,
                        principalTable: "Recruitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecruitmentVariant_Variants_VariantsId",
                        column: x => x.VariantsId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarEmployee",
                columns: table => new
                {
                    PassengerCarsId = table.Column<long>(type: "bigint", nullable: false),
                    PassengersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEmployee", x => new { x.PassengerCarsId, x.PassengersId });
                    table.ForeignKey(
                        name: "FK_CarEmployee_Cars_PassengerCarsId",
                        column: x => x.PassengerCarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarEmployee_Employees_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarEmployee1",
                columns: table => new
                {
                    DriverCarsId = table.Column<long>(type: "bigint", nullable: false),
                    DriversId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEmployee1", x => new { x.DriverCarsId, x.DriversId });
                    table.ForeignKey(
                        name: "FK_CarEmployee1_Cars_DriverCarsId",
                        column: x => x.DriverCarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarEmployee1_Employees_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompanyEmployee",
                columns: table => new
                {
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmployee", x => new { x.CompaniesId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_CompanyEmployee_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee_Document",
                columns: table => new
                {
                    DocumentsId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Document", x => new { x.DocumentsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_Employee_Document_Documents_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Document_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeHouse",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    HousesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHouse", x => new { x.EmployeesId, x.HousesId });
                    table.ForeignKey(
                        name: "FK_EmployeeHouse_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHouse_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeePlan",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    PlansId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePlan", x => new { x.EmployeesId, x.PlansId });
                    table.ForeignKey(
                        name: "FK_EmployeePlan_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePlan_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeePosition",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    PositionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePosition", x => new { x.EmployeesId, x.PositionsId });
                    table.ForeignKey(
                        name: "FK_EmployeePosition_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePosition_Positions_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeRealisation",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    RealisationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRealisation", x => new { x.EmployeesId, x.RealisationsId });
                    table.ForeignKey(
                        name: "FK_EmployeeRealisation_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRealisation_Realisations_RealisationsId",
                        column: x => x.RealisationsId,
                        principalTable: "Realisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActionFunctions_CompanyId",
                table: "ActionFunctions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionFunctions_MailId",
                table: "ActionFunctions",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionFunctions_PositionId",
                table: "ActionFunctions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionFunctions_StoreModelId",
                table: "ActionFunctions",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionFunctions_SystemFunctionId",
                table: "ActionFunctions",
                column: "SystemFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_Document_DocumentsId",
                table: "Car_Document",
                column: "DocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CarCompany_CompaniesId",
                table: "CarCompany",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_CarEmployee_PassengersId",
                table: "CarEmployee",
                column: "PassengersId");

            migrationBuilder.CreateIndex(
                name: "IX_CarEmployee1_DriversId",
                table: "CarEmployee1",
                column: "DriversId");

            migrationBuilder.CreateIndex(
                name: "IX_CarHouse_HousesId",
                table: "CarHouse",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRealisation_RealisationsId",
                table: "CarRealisation",
                column: "RealisationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_StoreModelId",
                table: "Cars",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_StatusId",
                table: "Companies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_StoreModelId",
                table: "Companies",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Document_DocumentsId",
                table: "Company_Document",
                column: "DocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployee_EmployeesId",
                table: "CompanyEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHouse_HousesId",
                table: "CompanyHouse",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRealisation_RealisationsId",
                table: "CompanyRealisation",
                column: "RealisationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StatusId",
                table: "Documents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StoreModelId",
                table: "Documents",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Document_EmployeesId",
                table: "Employee_Document",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHouse_HousesId",
                table: "EmployeeHouse",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePlan_PlansId",
                table: "EmployeePlan",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePosition_PositionsId",
                table: "EmployeePosition",
                column: "PositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRealisation_RealisationsId",
                table: "EmployeeRealisation",
                column: "RealisationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PreviousStatusId",
                table: "Employees",
                column: "PreviousStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RecruitmentContactId",
                table: "Employees",
                column: "RecruitmentContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RecruitmentId",
                table: "Employees",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StatusId",
                table: "Employees",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreModelId",
                table: "Employees",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_House_Document_HousesId",
                table: "House_Document",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_StoreModelId",
                table: "Houses",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_Document_MailsId",
                table: "Mail_Document",
                column: "MailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CompanyId",
                table: "Offers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ImageId",
                table: "Offers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PositionId",
                table: "Offers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CompanyId",
                table: "Plans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CompanyId",
                table: "Positions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ParentId",
                table: "Positions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PositionUnitId",
                table: "Positions",
                column: "PositionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_StoreModelId",
                table: "Positions",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisation_Document_RealisationsId",
                table: "Realisation_Document",
                column: "RealisationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_ActionFunctionId",
                table: "Realisations",
                column: "ActionFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_ComparatorId",
                table: "Realisations",
                column: "ComparatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_HouseId",
                table: "Realisations",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_PositionId",
                table: "Realisations",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_VariantId",
                table: "Realisations",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Realisations_VariantRealisationId",
                table: "Realisations",
                column: "VariantRealisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_DocumentType_RecruitmentsId",
                table: "Recruitment_DocumentType",
                column: "RecruitmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitments_CompanyId",
                table: "Recruitments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitments_OfferId",
                table: "Recruitments",
                column: "OfferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruitments_StatusId",
                table: "Recruitments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitments_StoreModelId",
                table: "Recruitments",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentVariant_VariantsId",
                table: "RecruitmentVariant",
                column: "VariantsId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusActions_ActionFunctionId",
                table: "StatusActions",
                column: "ActionFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusActions_StatusActionTriggerId",
                table: "StatusActions",
                column: "StatusActionTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StatusId",
                table: "Statuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StoreModelId",
                table: "Statuses",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusStatusAction_StatusesId",
                table: "StatusStatusAction",
                column: "StatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreModelSystemFunction_SystemFunctionsId",
                table: "StoreModelSystemFunction",
                column: "SystemFunctionsId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantRealisations_VariantId",
                table: "VariantRealisations",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_StatusId",
                table: "Variants",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_StoreModelId",
                table: "Variants",
                column: "StoreModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_VariantTypeId",
                table: "Variants",
                column: "VariantTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car_Document");

            migrationBuilder.DropTable(
                name: "CarCompany");

            migrationBuilder.DropTable(
                name: "CarEmployee");

            migrationBuilder.DropTable(
                name: "CarEmployee1");

            migrationBuilder.DropTable(
                name: "CarHouse");

            migrationBuilder.DropTable(
                name: "CarRealisation");

            migrationBuilder.DropTable(
                name: "Company_Document");

            migrationBuilder.DropTable(
                name: "CompanyEmployee");

            migrationBuilder.DropTable(
                name: "CompanyHouse");

            migrationBuilder.DropTable(
                name: "CompanyRealisation");

            migrationBuilder.DropTable(
                name: "Employee_Document");

            migrationBuilder.DropTable(
                name: "EmployeeHouse");

            migrationBuilder.DropTable(
                name: "EmployeePlan");

            migrationBuilder.DropTable(
                name: "EmployeePosition");

            migrationBuilder.DropTable(
                name: "EmployeeRealisation");

            migrationBuilder.DropTable(
                name: "House_Document");

            migrationBuilder.DropTable(
                name: "Mail_Document");

            migrationBuilder.DropTable(
                name: "Realisation_Document");

            migrationBuilder.DropTable(
                name: "Recruitment_DocumentType");

            migrationBuilder.DropTable(
                name: "RecruitmentVariant");

            migrationBuilder.DropTable(
                name: "StatusStatusAction");

            migrationBuilder.DropTable(
                name: "StoreModelSystemFunction");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Realisations");

            migrationBuilder.DropTable(
                name: "StatusActions");

            migrationBuilder.DropTable(
                name: "RecruitmentContacts");

            migrationBuilder.DropTable(
                name: "Recruitments");

            migrationBuilder.DropTable(
                name: "Comparators");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "VariantRealisations");

            migrationBuilder.DropTable(
                name: "ActionFunctions");

            migrationBuilder.DropTable(
                name: "StatusActionTriggers");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "SystemFunctions");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "VariantTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "PositionUnits");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "StoreModels");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ProfilePictureDataUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineeringFluidCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringFluidCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nozzles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemConnectedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EndType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominalDiameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OuterDiameter = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: false),
                    OuterDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThicknessUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeightDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nozzles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NozzleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NozzleTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationStrategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesoryImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesoryImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleInsideProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInsideProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariableInstrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierInstrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquivalentLenghPrice = table.Column<double>(type: "float", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborDayPrice = table.Column<double>(type: "float", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipeTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValveTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActuadorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasFeedBack = table.Column<bool>(type: "bit", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValveTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValveTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingAccesoryImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingCategorys_PipingAccesoryImages_PipingAccesoryImageId",
                        column: x => x.PipingAccesoryImageId,
                        principalTable: "PipingAccesoryImages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StakeHolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleInsideProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StakeHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StakeHolders_RoleInsideProjects_RoleInsideProjectId",
                        column: x => x.RoleInsideProjectId,
                        principalTable: "RoleInsideProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesoryCodeBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PipingCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesoryCodeBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingAccesoryCodeBrands_PipingCategorys_PipingCategoryId",
                        column: x => x.PipingCategoryId,
                        principalTable: "PipingCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingAccesorys_PipingCategorys_PipingCategoryId",
                        column: x => x.PipingCategoryId,
                        principalTable: "PipingCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNeedType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialBudget = table.Column<double>(type: "float", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SponsorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Version0Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingTab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNodeOpen = table.Column<bool>(type: "bit", nullable: false),
                    Tab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_StakeHolders_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_StakeHolders_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingConnectionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominalDiameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OuterDiameter = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: false),
                    OuterDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThicknessUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingAccesoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccesoryConnectionSide = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingConnectionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingConnectionTypes_PipingAccesorys_PipingAccesoryId",
                        column: x => x.PipingAccesoryId,
                        principalTable: "PipingAccesorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationStrategyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsNodeOpen = table.Column<bool>(type: "bit", nullable: false),
                    Tab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_OrganizationStrategies_OrganizationStrategyId",
                        column: x => x.OrganizationStrategyId,
                        principalTable: "OrganizationStrategies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HighLevelRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighLevelRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighLevelRequirements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StakeHolderIssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StakeHolderResponsibleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SolveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HowToSolveIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLogs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLogs_StakeHolders_StakeHolderIssuerId",
                        column: x => x.StakeHolderIssuerId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueLogs_StakeHolders_StakeHolderResponsibleId",
                        column: x => x.StakeHolderResponsibleId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LearnedLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnedLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnedLessons_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofMeeting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeetingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsNodeOpen = table.Column<bool>(type: "bit", nullable: false),
                    Tab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStakeHolder",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StakeHoldersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStakeHolder", x => new { x.ProjectsId, x.StakeHoldersId });
                    table.ForeignKey(
                        name: "FK_ProjectStakeHolder_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStakeHolder_StakeHolders_StakeHoldersId",
                        column: x => x.StakeHoldersId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WBSComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubComponentRelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WBSComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WBSComponents_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WBSComponents_WBSComponents_SubComponentRelationId",
                        column: x => x.SubComponentRelationId,
                        principalTable: "WBSComponents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BackGrounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackGrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackGrounds_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecissionCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecissionCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecissionCriterias_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertJudgements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpertId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertJudgements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertJudgements_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertJudgements_StakeHolders_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KnownRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnownRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnownRisks_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNodeOpen = table.Column<bool>(type: "bit", nullable: false),
                    Tab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scopes_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SucessfullFactors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucessfullFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SucessfullFactors_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingAgreements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAgreements_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingAttendants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StakeHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAttendants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAttendants_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingAttendants_StakeHolders_StakeHolderId",
                        column: x => x.StakeHolderId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliverables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNodeOpen = table.Column<bool>(type: "bit", nullable: false),
                    Tab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliverables_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcceptanceCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptanceCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcceptanceCriterias_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alterations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alterations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alterations_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alterations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assumptions_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bennefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bennefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bennefits_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Constrainsts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constrainsts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constrainsts_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contingencys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contingencys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contingencys_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contingencys_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliverableRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverableRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverableRisks_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EHSs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EHSs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EHSs_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EHSs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Electricals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electricals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Electricals_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Electricals_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engineerings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineerings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engineerings_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Engineerings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foundations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foundations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foundations_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foundations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paintings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paintings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paintings_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paintings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessFlowDiagrams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessFlowDiagrams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessFlowDiagrams_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessFlowDiagrams_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResponsibleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requirements_StakeHolders_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requirements_StakeHolders_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Structurals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structurals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structurals_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Structurals_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxes_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Taxes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitaryCost = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testings_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Testings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProcessFlowDiagramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTemplates_EquipmentTemplateId",
                        column: x => x.EquipmentTemplateId,
                        principalTable: "EquipmentTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                        column: x => x.ProcessFlowDiagramId,
                        principalTable: "ProcessFlowDiagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProcessFlowDiagramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instruments_InstrumentTemplates_InstrumentTemplateId",
                        column: x => x.InstrumentTemplateId,
                        principalTable: "InstrumentTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instruments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                        column: x => x.ProcessFlowDiagramId,
                        principalTable: "ProcessFlowDiagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instruments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Isometrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProcessFlowDiagramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialUnitaryCost = table.Column<double>(type: "float", nullable: false),
                    MaterialQuantity = table.Column<double>(type: "float", nullable: false),
                    LaborUnitaryCost = table.Column<double>(type: "float", nullable: false),
                    LaborQuantity = table.Column<double>(type: "float", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FluidCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insulation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isometrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Isometrics_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Isometrics_EngineeringFluidCodes_FluidCodeId",
                        column: x => x.FluidCodeId,
                        principalTable: "EngineeringFluidCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Isometrics_ProcessFlowDiagrams_ProcessFlowDiagramId",
                        column: x => x.ProcessFlowDiagramId,
                        principalTable: "ProcessFlowDiagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Isometrics_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProcessFlowDiagramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValveTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valves_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Valves_ProcessFlowDiagrams_ProcessFlowDiagramId",
                        column: x => x.ProcessFlowDiagramId,
                        principalTable: "ProcessFlowDiagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Valves_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valves_ValveTemplates_ValveTemplateId",
                        column: x => x.ValveTemplateId,
                        principalTable: "ValveTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxesItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxesItems_Taxes_TaxItemId",
                        column: x => x.TaxItemId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IsometricItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsometricId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PipingAccesoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsometricItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IsometricItems_Isometrics_IsometricId",
                        column: x => x.IsometricId,
                        principalTable: "Isometrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsometricItems_PipingAccesorys_PipingAccesoryId",
                        column: x => x.PipingAccesoryId,
                        principalTable: "PipingAccesorys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcceptanceCriterias_DeliverableId",
                table: "AcceptanceCriterias",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Alterations_DeliverableId",
                table: "Alterations",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Alterations_ProjectId",
                table: "Alterations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assumptions_DeliverableId",
                table: "Assumptions",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_BackGrounds_CaseId",
                table: "BackGrounds",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bennefits_DeliverableId",
                table: "Bennefits",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OrganizationStrategyId",
                table: "Cases",
                column: "OrganizationStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ProjectId",
                table: "Cases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Constrainsts_DeliverableId",
                table: "Constrainsts",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Contingencys_DeliverableId",
                table: "Contingencys",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Contingencys_ProjectId",
                table: "Contingencys",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DecissionCriterias_CaseId",
                table: "DecissionCriterias",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRisks_DeliverableId",
                table: "DeliverableRisks",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverables_ScopeId",
                table: "Deliverables",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_EHSs_DeliverableId",
                table: "EHSs",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_EHSs_ProjectId",
                table: "EHSs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Electricals_DeliverableId",
                table: "Electricals",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Electricals_ProjectId",
                table: "Electricals",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineerings_DeliverableId",
                table: "Engineerings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineerings_ProjectId",
                table: "Engineerings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_DeliverableId",
                table: "Equipments",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTemplateId",
                table: "Equipments",
                column: "EquipmentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ProcessFlowDiagramId",
                table: "Equipments",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ProjectId",
                table: "Equipments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_BrandTemplateId",
                table: "EquipmentTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertJudgements_CaseId",
                table: "ExpertJudgements",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertJudgements_ExpertId",
                table: "ExpertJudgements",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_DeliverableId",
                table: "Foundations",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_ProjectId",
                table: "Foundations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HighLevelRequirements_ProjectId",
                table: "HighLevelRequirements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_DeliverableId",
                table: "Instruments",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_InstrumentTemplateId",
                table: "Instruments",
                column: "InstrumentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ProcessFlowDiagramId",
                table: "Instruments",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ProjectId",
                table: "Instruments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTemplates_BrandTemplateId",
                table: "InstrumentTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_IsometricItems_IsometricId",
                table: "IsometricItems",
                column: "IsometricId");

            migrationBuilder.CreateIndex(
                name: "IX_IsometricItems_PipingAccesoryId",
                table: "IsometricItems",
                column: "PipingAccesoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_DeliverableId",
                table: "Isometrics",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_FluidCodeId",
                table: "Isometrics",
                column: "FluidCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_ProcessFlowDiagramId",
                table: "Isometrics",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_ProjectId",
                table: "Isometrics",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_ProjectId",
                table: "IssueLogs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_StakeHolderIssuerId",
                table: "IssueLogs",
                column: "StakeHolderIssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_StakeHolderResponsibleId",
                table: "IssueLogs",
                column: "StakeHolderResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_KnownRisks_CaseId",
                table: "KnownRisks",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnedLessons_ProjectId",
                table: "LearnedLessons",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgreements_MeetingId",
                table: "MeetingAgreements",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAttendants_MeetingId",
                table: "MeetingAttendants",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAttendants_StakeHolderId",
                table: "MeetingAttendants",
                column: "StakeHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ProjectId",
                table: "Meetings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Nozzles_EngineeringItemId",
                table: "Nozzles",
                column: "EngineeringItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Nozzles_ItemConnectedId",
                table: "Nozzles",
                column: "ItemConnectedId");

            migrationBuilder.CreateIndex(
                name: "IX_NozzleTemplates_TemplateId",
                table: "NozzleTemplates",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Paintings_DeliverableId",
                table: "Paintings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Paintings_ProjectId",
                table: "Paintings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeTemplates_BrandTemplateId",
                table: "PipeTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingAccesoryCodeBrands_PipingCategoryId",
                table: "PipingAccesoryCodeBrands",
                column: "PipingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingAccesorys_PipingCategoryId",
                table: "PipingAccesorys",
                column: "PipingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingCategorys_PipingAccesoryImageId",
                table: "PipingCategorys",
                column: "PipingAccesoryImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingConnectionTypes_PipingAccesoryId",
                table: "PipingConnectionTypes",
                column: "PipingAccesoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowDiagrams_DeliverableId",
                table: "ProcessFlowDiagrams",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowDiagrams_ProjectId",
                table: "ProcessFlowDiagrams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SponsorId",
                table: "Projects",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStakeHolder_StakeHoldersId",
                table: "ProjectStakeHolder",
                column: "StakeHoldersId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_DeliverableId",
                table: "Requirements",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_RequestedById",
                table: "Requirements",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ResponsibleId",
                table: "Requirements",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Scopes_CaseId",
                table: "Scopes",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_StakeHolders_RoleInsideProjectId",
                table: "StakeHolders",
                column: "RoleInsideProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Structurals_DeliverableId",
                table: "Structurals",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Structurals_ProjectId",
                table: "Structurals",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SucessfullFactors_CaseId",
                table: "SucessfullFactors",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_DeliverableId",
                table: "Taxes",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_ProjectId",
                table: "Taxes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxesItems_SelectedId",
                table: "TaxesItems",
                column: "SelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxesItems_TaxItemId",
                table: "TaxesItems",
                column: "TaxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Testings_DeliverableId",
                table: "Testings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Testings_ProjectId",
                table: "Testings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_DeliverableId",
                table: "Valves",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ProcessFlowDiagramId",
                table: "Valves",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ProjectId",
                table: "Valves",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ValveTemplateId",
                table: "Valves",
                column: "ValveTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ValveTemplates_BrandTemplateId",
                table: "ValveTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WBSComponents_ProjectId",
                table: "WBSComponents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WBSComponents_SubComponentRelationId",
                table: "WBSComponents",
                column: "SubComponentRelationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptanceCriterias");

            migrationBuilder.DropTable(
                name: "Alterations");

            migrationBuilder.DropTable(
                name: "AppStates");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assumptions");

            migrationBuilder.DropTable(
                name: "BackGrounds");

            migrationBuilder.DropTable(
                name: "Bennefits");

            migrationBuilder.DropTable(
                name: "Constrainsts");

            migrationBuilder.DropTable(
                name: "Contingencys");

            migrationBuilder.DropTable(
                name: "DecissionCriterias");

            migrationBuilder.DropTable(
                name: "DeliverableRisks");

            migrationBuilder.DropTable(
                name: "EHSs");

            migrationBuilder.DropTable(
                name: "Electricals");

            migrationBuilder.DropTable(
                name: "Engineerings");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "ExpertJudgements");

            migrationBuilder.DropTable(
                name: "Foundations");

            migrationBuilder.DropTable(
                name: "HighLevelRequirements");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "IsometricItems");

            migrationBuilder.DropTable(
                name: "IssueLogs");

            migrationBuilder.DropTable(
                name: "KnownRisks");

            migrationBuilder.DropTable(
                name: "LearnedLessons");

            migrationBuilder.DropTable(
                name: "MeetingAgreements");

            migrationBuilder.DropTable(
                name: "MeetingAttendants");

            migrationBuilder.DropTable(
                name: "Nozzles");

            migrationBuilder.DropTable(
                name: "NozzleTemplates");

            migrationBuilder.DropTable(
                name: "Paintings");

            migrationBuilder.DropTable(
                name: "PipeTemplates");

            migrationBuilder.DropTable(
                name: "PipingAccesoryCodeBrands");

            migrationBuilder.DropTable(
                name: "PipingConnectionTypes");

            migrationBuilder.DropTable(
                name: "ProjectStakeHolder");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "Structurals");

            migrationBuilder.DropTable(
                name: "SucessfullFactors");

            migrationBuilder.DropTable(
                name: "TaxesItems");

            migrationBuilder.DropTable(
                name: "Testings");

            migrationBuilder.DropTable(
                name: "Valves");

            migrationBuilder.DropTable(
                name: "WBSComponents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EquipmentTemplates");

            migrationBuilder.DropTable(
                name: "InstrumentTemplates");

            migrationBuilder.DropTable(
                name: "Isometrics");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "PipingAccesorys");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "ValveTemplates");

            migrationBuilder.DropTable(
                name: "EngineeringFluidCodes");

            migrationBuilder.DropTable(
                name: "ProcessFlowDiagrams");

            migrationBuilder.DropTable(
                name: "PipingCategorys");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Deliverables");

            migrationBuilder.DropTable(
                name: "PipingAccesoryImages");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "OrganizationStrategies");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "StakeHolders");

            migrationBuilder.DropTable(
                name: "RoleInsideProjects");
        }
    }
}

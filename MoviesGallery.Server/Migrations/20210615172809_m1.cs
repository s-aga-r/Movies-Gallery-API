using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesGallery.Server.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmStars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmStars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StoryLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<float>(type: "real", nullable: false),
                    TotalDownloads = table.Column<long>(type: "bigint", nullable: false),
                    SampleDownloadLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfRelease = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IMDB = table.Column<float>(type: "real", nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QualityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMovie",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMovie", x => new { x.CategoriesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadLinks_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmStarMovie",
                columns: table => new
                {
                    FilmStarsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmStarMovie", x => new { x.FilmStarsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_FilmStarMovie_FilmStars_FilmStarsId",
                        column: x => x.FilmStarsId,
                        principalTable: "FilmStars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmStarMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageMovie",
                columns: table => new
                {
                    LanguagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageMovie", x => new { x.LanguagesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_LanguageMovie_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenshots_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlideShows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateOfUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideShows_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("0ecfbcfd-7d58-4372-8345-c3b16407e3e4"), null, true, "Category 1" },
                    { new Guid("d662a23e-72c8-4b3e-8d5e-65cf3a79e043"), null, true, "Category 10" },
                    { new Guid("61ead6da-bc51-4e6c-86d2-09041e86c5de"), null, true, "Category 8" },
                    { new Guid("d1a443f0-b528-4295-8cea-96fe0c6e29fb"), null, true, "Category 7" },
                    { new Guid("a29f4348-7744-4c7f-a838-cb6a2f6ff2ba"), null, true, "Category 6" },
                    { new Guid("6adf8036-0581-4658-8acf-8239ca5eedcf"), null, true, "Category 9" },
                    { new Guid("7b154460-1527-4d5c-8643-4411d0b6016e"), null, true, "Category 4" },
                    { new Guid("e369bca6-3939-418f-bb32-5e66327bdc4c"), null, true, "Category 3" },
                    { new Guid("e3dea670-ae23-49ed-959e-e80ea6099bc5"), null, true, "Category 2" },
                    { new Guid("501a8bfe-6bfd-4ee6-9625-737394980145"), null, true, "Category 5" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("f95141a6-4a75-4450-9449-b00189d331cc"), null, true, "France" },
                    { new Guid("8566e1c7-4f2a-48d9-8ebe-82a77cef4ac4"), null, true, "India" },
                    { new Guid("abe4d049-487b-45f4-981f-15837e1f605a"), null, true, "Nigeria" },
                    { new Guid("4d9bb00e-bb68-4b65-9975-f93f557307c1"), null, true, "USA" },
                    { new Guid("7904ca26-82ad-4b85-91eb-9b8c24da0991"), null, true, "China" },
                    { new Guid("9898f3fa-8936-4585-8de0-f420b6e0bc03"), null, true, "Japan" },
                    { new Guid("1f7659d2-8cc5-4edd-89bd-86f4c44c7298"), null, true, "South Korea" },
                    { new Guid("a30f7213-6b2d-42a9-a4eb-0e8c4af58828"), null, true, "UK" },
                    { new Guid("d41067b6-4ed0-4e2a-b70e-fdf5696900a3"), null, true, "Spain" },
                    { new Guid("fdb5df10-ec01-4593-86a5-cb231f4df924"), null, true, "Italy" },
                    { new Guid("ca738e7d-001d-4a43-ac93-d2fba60508b2"), null, true, "Russia" },
                    { new Guid("e63f3edf-a95f-457e-92f1-759efaeb2f9d"), null, true, "Argentina" },
                    { new Guid("69092c55-4faa-4632-a7d5-0a250394b671"), null, true, "Brazil" },
                    { new Guid("117bfe16-c1fc-4799-ac61-c07ef3af72d6"), null, true, "Canada" },
                    { new Guid("c4f91b32-10af-4600-bc95-7e54ab78171d"), null, true, "Germany" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("a775e58d-6181-4ba4-95a2-9fdf120e3ee4"), null, true, "Director 10" },
                    { new Guid("e8a81d0e-6a77-4175-a277-ef722b43bccb"), null, true, "Director 9" },
                    { new Guid("4ec85ffa-963d-4f5d-9478-a88b4ced17ba"), null, true, "Director 8" },
                    { new Guid("7010fb31-094f-4363-90aa-c559e3aa76ce"), null, true, "Director 7" },
                    { new Guid("21880c20-149c-4b74-91ca-8d7d04f114d2"), null, true, "Director 6" },
                    { new Guid("5f2c1e52-149a-49a4-9d5b-f236c792411c"), null, true, "Director 4" },
                    { new Guid("18da6f78-e697-4793-9715-bca3a91e403d"), null, true, "Director 3" },
                    { new Guid("cc556ea8-6a2e-4baf-b335-0ef6a76abcff"), null, true, "Director 2" },
                    { new Guid("e43326bb-f2b3-45a0-8a32-5c5b70e0164c"), null, true, "Director 1" },
                    { new Guid("3a9eb2a6-88da-4710-8fd5-73182c64f19d"), null, true, "Director 5" }
                });

            migrationBuilder.InsertData(
                table: "FilmStars",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("025cd4ad-479e-4ff0-b44a-c9f96e35f536"), null, true, "FilmStar 8" },
                    { new Guid("a244649e-08a3-4a4a-89aa-9b0f5a924a35"), null, true, "FilmStar 10" },
                    { new Guid("1a84419c-0543-4fb8-98f8-41646447c1eb"), null, true, "FilmStar 9" },
                    { new Guid("f4b03c83-e21b-42fe-bff9-f4ea2b5f6fdc"), null, true, "FilmStar 6" },
                    { new Guid("b53b8f5a-ddf7-4866-bf83-3380f72a5bd3"), null, true, "FilmStar 7" },
                    { new Guid("31352758-2bc4-424d-8a30-00fcf01c4cf8"), null, true, "FilmStar 4" },
                    { new Guid("012694ad-2e99-4b1f-bd93-36300819d70b"), null, true, "FilmStar 3" }
                });

            migrationBuilder.InsertData(
                table: "FilmStars",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("bd994c16-b011-4a1f-b940-80aa3a4501f0"), null, true, "FilmStar 2" },
                    { new Guid("abddab8c-4dfa-4531-aae9-7527e9677d25"), null, true, "FilmStar 1" },
                    { new Guid("6ae21efc-68c2-4ffa-b6c3-8ce75b4ea282"), null, true, "FilmStar 5" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("54f429e6-8864-447c-955d-6db44725acc0"), null, true, "Persian" },
                    { new Guid("7fc551bd-0efa-4def-8c9d-b7ece78e1d27"), null, true, "Danish" },
                    { new Guid("8332fc8f-e84c-43dd-b7a0-fb4ea3cd1372"), null, true, "Latin" },
                    { new Guid("5e1b8e58-2d8a-4411-92f3-af7e90bd43ca"), null, true, "Swedish" },
                    { new Guid("477b6732-dade-4a2e-a2c2-b0759d172a88"), null, true, "Portuguese" },
                    { new Guid("34699ed5-ac62-47ff-bd7f-74ec8af00fc7"), null, true, "Cantonese" },
                    { new Guid("2acae217-4638-4b01-b129-b6492d0ccc1a"), null, true, "Hebrew" },
                    { new Guid("23ef98d3-c998-4bc4-a8df-9d46f4b99440"), null, true, "Korean" },
                    { new Guid("23297f52-8150-4e55-aa43-3e347a0485f3"), null, true, "Arabic" },
                    { new Guid("9c079324-4650-4b2b-a23d-f95d6c88a0e4"), null, true, "Ukrainian" },
                    { new Guid("574d6dd7-c405-4d25-be19-136aa0f9296a"), null, true, "Italian" },
                    { new Guid("927e0e9d-1a84-45da-ad12-674ed25ab40a"), null, true, "Japanese" },
                    { new Guid("9551851d-bfad-4a58-8403-d384959fff8d"), null, true, "Mandarin" },
                    { new Guid("ff136ca3-bf19-455a-bdd3-68efe022f9c3"), null, true, "English" },
                    { new Guid("c1a595be-344e-4bc0-a61d-34eea1c9b591"), null, true, "Hindi" },
                    { new Guid("58e554d0-b10b-4172-ae53-a09436fcc8e1"), null, true, "German" },
                    { new Guid("77793fdc-cc93-45e4-91ec-814023d28a08"), null, true, "Spanish" },
                    { new Guid("18fc1c60-ff29-4e80-9bb1-22851fbcad8b"), null, true, "French" },
                    { new Guid("d91b6f84-9b3a-42f4-a9ab-742ad88130ba"), null, true, "Russian" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CountryId", "CoverImage", "DateOfRelease", "DateOfUpload", "Description", "DirectorId", "IMDB", "IsActive", "LastUpdatedOn", "QualityId", "SampleDownloadLink", "Size", "StoryLine", "Title", "TotalDownloads", "Trailer" },
                values: new object[,]
                {
                    { new Guid("21b37cbe-3bbe-412c-a8cd-f590de3a3bf6"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3885), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3886), null, null, 0f, null, "Movie 10", 0L, null },
                    { new Guid("71661d2d-1ba0-40af-878d-65a395da03b5"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3878), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3879), null, null, 0f, null, "Movie 9", 0L, null },
                    { new Guid("49e9eb61-f776-44e3-a36e-9404c5bb51a4"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3875), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3876), null, null, 0f, null, "Movie 8", 0L, null },
                    { new Guid("533e3b70-171c-48c3-83c4-2e8b4bce6578"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3873), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3873), null, null, 0f, null, "Movie 7", 0L, null },
                    { new Guid("2299ec48-d4b3-4656-bec1-b8315e9d236f"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3869), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3870), null, null, 0f, null, "Movie 6", 0L, null },
                    { new Guid("561abf17-b95c-4a68-83ac-9f52eb84224f"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3860), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3861), null, null, 0f, null, "Movie 4", 0L, null },
                    { new Guid("0fa77b4d-f011-40f3-935e-5c2ce4735aca"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3857), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3858), null, null, 0f, null, "Movie 3", 0L, null },
                    { new Guid("a74f1628-7153-4392-86f2-f6054515fccd"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3841), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3845), null, null, 0f, null, "Movie 2", 0L, null },
                    { new Guid("641a35ab-9ebb-4d10-b8ea-979f1be734b3"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 752, DateTimeKind.Local).AddTicks(5239), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3077), null, null, 0f, null, "Movie 1", 0L, null },
                    { new Guid("819f2f78-7d8a-4453-8538-85c0958dbb20"), null, null, null, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3863), null, null, 0f, true, new DateTime(2021, 6, 15, 22, 58, 8, 753, DateTimeKind.Local).AddTicks(3864), null, null, 0f, null, "Movie 5", 0L, null }
                });

            migrationBuilder.InsertData(
                table: "Qualities",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("cd43b799-c026-4955-a00b-a770827f9319"), null, true, "Quality 9" },
                    { new Guid("0085ff54-943b-4bd1-9b36-3b0208dacc67"), null, true, "Quality 1" },
                    { new Guid("d67f32e8-24cd-459b-b3c4-9065755502c8"), null, true, "Quality 2" },
                    { new Guid("a3485b87-b8e8-4388-95e8-c3e56c019c05"), null, true, "Quality 3" },
                    { new Guid("4c4d68e1-47cd-4af2-96f4-cac0088a3f17"), null, true, "Quality 4" },
                    { new Guid("43c43fec-332a-4212-8d91-321321d1d2ab"), null, true, "Quality 5" },
                    { new Guid("9986dd8e-a99a-4e01-852f-e5b8f22bd74c"), null, true, "Quality 6" },
                    { new Guid("a54b5620-9edd-40e0-9ee1-0305004db426"), null, true, "Quality 7" },
                    { new Guid("aabbbc6c-c5f8-4b74-93a5-422db95d003e"), null, true, "Quality 8" },
                    { new Guid("9ad582b5-532d-4ac9-b1c9-92c6966e1bcc"), null, true, "Quality 10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMovie_MoviesId",
                table: "CategoryMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadLinks_MovieId",
                table: "DownloadLinks",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmStarMovie_MoviesId",
                table: "FilmStarMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageMovie_MoviesId",
                table: "LanguageMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CountryId",
                table: "Movies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_QualityId",
                table: "Movies",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_MovieId",
                table: "Screenshots",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideShows_MovieId",
                table: "SlideShows",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMovie");

            migrationBuilder.DropTable(
                name: "DownloadLinks");

            migrationBuilder.DropTable(
                name: "FilmStarMovie");

            migrationBuilder.DropTable(
                name: "LanguageMovie");

            migrationBuilder.DropTable(
                name: "Screenshots");

            migrationBuilder.DropTable(
                name: "SlideShows");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FilmStars");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Qualities");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyBook.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    SubType = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    SubId = table.Column<int>(type: "integer", nullable: false),
                    SubDateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Subs_SubId",
                        column: x => x.SubId,
                        principalTable: "Subs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "BookUser",
                columns: table => new
                {
                    FavoriteBooksId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.FavoriteBooksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BookUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Books_FavoriteBooksId",
                        column: x => x.FavoriteBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("12d534a7-4535-4819-8704-bcfd7553ab46"), "579343d8-88e4-454f-8841-dffe590dddd8", "User", "USER" },
                    { new Guid("6dc02633-d464-4f86-8575-4cb190d670a6"), "24c6ffb7-7f92-433c-bebf-4a90f3b9a536", "UserSub", "USERSUB" },
                    { new Guid("6f17d951-3ad5-49f9-b333-2a37e367333d"), "1bb26b08-9504-4d58-bf23-6b230ed9d589", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "FullName", "Image" },
                values: new object[,]
                {
                    { new Guid("02788b50-5eae-42ce-a375-c0416840d687"), "Американский автор и консультант по личному развитию, предприниматель и блогер. Ведет блог под своим именем на одноименном сайте. По состоянию на 2019 год написал три книги. Книга «Тонкое искусство пофигизма» заняла шестое место в списке бестселлеров The New York Times.", "Марк Мэнсон", new byte[0] },
                    { new Guid("2ee0cdd2-a3d6-414f-9038-874b12916a86"), "Джордж Оруэлл (George Orwell) – творческий псевдоним английского писателя и публициста. Настоящее имя – Эрик Артур Блэр (Eric Arthur Blair). Родился 25 июня 1903 года в Индии в семье британского торгового агента. Оруэлл учился в школе св. Киприана. В 1917 году получил именную стипендию и до 1921 года посещал Итонский Колледж. Жил в Великобритании и других странах Европы, где перебивался случайными заработками и начал писать. Пять лет служил в колониальной полиции в Бирме, про что в 1934 году рассказал в повести «Дни в Бирме».", "Джордж Оруэлл", new byte[0] },
                    { new Guid("320852a1-b75b-4b89-b286-873c80d11727"), " английская писательница. Автор более двух десятков книг, носитель множества почётных учёных степеней различных университетов и лауреат многочисленных литературных наград и премий.", "Антония Сьюзен Байетт", new byte[0] },
                    { new Guid("51e7d2f1-d989-4e59-86c8-278123f564ea"), "Американская писательница, оратор и тренер по успеху.", "Джен Синсеро", new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "Subs",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка", 43200, "Месяц", 349m },
                    { 2, "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка", 259200, "Полгода", 1794m },
                    { 3, "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка", 525600, "Год", 2988m },
                    { 4, "Нихуя нет у тебя браток", 0, "Нет", 0m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SubDateStart", "SubId", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4bee3a36-db98-4071-ad61-a61db810decb"), 0, "784e7067-3603-4e5e-97ce-b70957a864ae", "1@mail.ru", false, new byte[0], "LastName", true, null, "Name", null, null, "AQAAAAEAACcQAAAAEBYiodokZsZRb23HmsOebO9xUQixijVwVPzaOSiF9yKPiVUTUBkr6WkcMsCaN9qsvQ", null, false, "6XN27C5W5ARJZESDVSRBUS4NMCN5XCPR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "S1mple" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genre", "Image", "Price", "SubType", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("16d8568d-ec3f-4ee4-9823-514d2c4daf17"), new Guid("320852a1-b75b-4b89-b286-873c80d11727"), "«Обладать» – один из лучших английских романов конца XX века и, несомненно, лучшее произведение Антонии Байетт. Впрочем, слово «роман» можно применить к этой удивительной прозе весьма условно. Что же такое перед нами? Детективный роман идей? Женский готический роман в современном исполнении? Рыцарский роман на новый лад? Все вместе – и нечто большее, глубоко современная вещь, вобравшая многие традиции и одновременно отмеченная печатью подлинного вдохновения и новаторства. В ней разными гранями переливается тайна английского духа и английского величия.", "Современная зарубежная литература", new byte[0], 549m, 1, "Обладать", 2016 },
                    { new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"), new Guid("320852a1-b75b-4b89-b286-873c80d11727"), "«Рагнарёк» – книга из серии древних мифов, переосмысленных современными писателями из разных стран", "Мифы", new byte[0], 549m, 1, "Рагнарёк", 2022 },
                    { new Guid("8faa5631-6f76-437a-a924-1c5ad5806a5e"), new Guid("02788b50-5eae-42ce-a375-c0416840d687"), "Современное общество пропагандирует культ успеха: будь умнее, богаче, продуктивнее – будь лучше всех. Соцсети изобилуют историями на тему, как какой-то малец придумал приложение и заработал кучу денег, статьями в духе «Тысяча и один способ быть счастливым», а фото во френдленте создают впечатление, что окружающие живут лучше и интереснее, чем мы. Однако наша зацикленность на позитиве и успехе лишь напоминает о том, чего мы не достигли, о мечтах, которые не сбылись. Как же стать по-настоящему счастливым? Популярный блогер Марк Мэнсон в книге «Тонкое искусство пофигизма» предлагает свой, оригинальный подход к этому вопросу. Его жизненная философия проста – необходимо научиться искусству пофигизма. Определив то, до чего вам действительно есть дело, нужно уметь наплевать на все второстепенное, забить на трудности, послать к черту чужое мнение и быть готовым взглянуть в лицо неудачам и показать им средний палец.", "Историческая фантастика", new byte[0], 549m, 1, "Тонкое искусство пофигизма", 2017 },
                    { new Guid("cca20620-56c4-40d3-bfc3-7d88bff9ea1f"), new Guid("2ee0cdd2-a3d6-414f-9038-874b12916a86"), "Фантастическая антиутопия Оруэлла – это мир тотального контроля и страха, где люди живут ради войны, ради того, чтобы скрыть правду и воспитать настоящих патриотов. В жестоком тоталитарном государстве люди лишены гражданских прав и собственного мнения. Культ Большого Брата подразумевает жесткую социальную иерархию, где даже проявление любви считается мыслепреступлением. Уинстон Смит понимает, что он пешка в этой истории, и тем не менее пытается что-то изменить. «1984» – это аллюзия на страны, в которых господствовал тоталитаризм. В ХХ веке книга была запрещена в социалистических государствах и вызвала волну возмущения по всему миру. Сейчас же «1984» считается ключевым произведением в жанре антиутопии.", "Историческая фантастика", new byte[0], 229m, 1, "1984", 2021 },
                    { new Guid("fa8bbaf0-a3f9-4378-84bf-dccc9ecc2155"), new Guid("51e7d2f1-d989-4e59-86c8-278123f564ea"), "Мышление формирует реальность вокруг нас. Даже в глубоком кризисе можно увидеть новые возможности. Меняйте мышление с помощью «НИ СЫ» и двигайтесь к новым целям.", "Саморазвитие", new byte[0], 549m, 1, "НИ СЫ. Будь уверен в своих силах и не позволяй сомнениям мешать тебе двигаться вперед...", 2018 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6f17d951-3ad5-49f9-b333-2a37e367333d"), new Guid("4bee3a36-db98-4071-ad61-a61db810decb") });

            migrationBuilder.InsertData(
                table: "BookUser",
                columns: new[] { "FavoriteBooksId", "UsersId" },
                values: new object[] { new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"), new Guid("4bee3a36-db98-4071-ad61-a61db810decb") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUsers_SubId",
                table: "AspNetUsers",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersId",
                table: "BookUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Subs");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}

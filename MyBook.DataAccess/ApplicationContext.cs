using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess;

//dotnet ef migrations add [Название комита] -s .\MyBook\ -p .\MyBook.DataAccess\
//dotnet ef database update -s .\MyBook\ -p .\MyBook.DataAccess\

public class ApplicationContext : IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Subscription> Subs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Subscriptions

        modelBuilder.Entity<Subscription>().HasData(
            new
            {
                Id = 1,
                Name = "Месяц",
                Duration = 60 * 24 * 30,
                Description =
                    "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 349m
            },
            new
            {
                Id = 2,
                Name = "Полгода",
                Duration = 60 * 24 * 180,
                Description =
                    "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 299m * 6
            },
            new
            {
                Id = 3,
                Name = "Год",
                Duration = 60 * 24 * 365,
                Description =
                    "📚  Все книги\n🎙️ Все аудиокниги и подкасты\n💌  Персональные рекомендации\n👌  Первоклассная поддержка",
                Price = 249m * 12
            }
        );

        #endregion

        #region Users

        modelBuilder.Entity<User>(b =>
        {
            b.HasData(new
            {
                Id = "4bee3a36-db98-4071-ad61-a61db810decb",
                UserName = "S1mple",
                Email = "1@mail.ru",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEBYiodokZsZRb23HmsOebO9xUQixijVwVPzaOSiF9yKPiVUTUBkr6WkcMsCaN9qsvQ",
                SecurityStamp = "6XN27C5W5ARJZESDVSRBUS4NMCN5XCPR",
                ConcurrencyStamp = "784e7067-3603-4e5e-97ce-b70957a864ae",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Image = Array.Empty<byte>(),
                LastName = "LastName",
                Name = "Name",
                SubId = 1,
                SubDateStart = default(DateTime)
            });
        });

        #endregion

        #region Authors

        modelBuilder.Entity<Author>().HasData(
            new
            {
                Id = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                FullName = "Антония Сьюзен Байетт",
                Description =
                    " английская писательница. Автор более двух десятков книг, носитель множества почётных учёных степеней различных университетов и лауреат многочисленных литературных наград и премий.",
                Image = Array.Empty<byte>()
            });

        #endregion

        #region Books

        modelBuilder.Entity<Book>().HasData(
            new
            {
                Id = new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"),
                Title = "Рагнарёк",
                AuthorId = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                Description =
                    "«Рагнарёк» – книга из серии древних мифов, переосмысленных современными писателями из разных стран",
                Genre = "Мифы",
                Year = 2022,
                Price = 549m,
                SubType = 1,
                Image = Array.Empty<byte>(),
            });

        #endregion
        
        #region BookUser
        
        modelBuilder.Entity<Book>()
            .HasMany(p => p.Users)
            .WithMany(p => p.FavoriteBooks)
            .UsingEntity(j => j.HasData(
                new
                {
                    FavoriteBooksId = new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"),
                    UsersId = "4bee3a36-db98-4071-ad61-a61db810decb"
                }
            ));
        
        #endregion
    }
    
}
using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasData(new
            {
                Id = Guid.Parse("4bee3a36-db98-4071-ad61-a61db810decb"),
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
    }
}
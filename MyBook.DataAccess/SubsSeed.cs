using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess;

public partial class Seeds
{
    public static void CreateSubs(ModelBuilder modelBuilder)
    {
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
    }
}
using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess;

public partial class Seeds
{
    public static void CreateBooks(ModelBuilder modelBuilder)
    {
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
    }
}
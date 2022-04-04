using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess;

public partial class Seeds
{
    public static void CreateAuthors(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new
            {
                Id = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                FullName = "Антония Сьюзен Байетт",
                Description =
                    " английская писательница. Автор более двух десятков книг, носитель множества почётных учёных степеней различных университетов и лауреат многочисленных литературных наград и премий.",
                Image = Array.Empty<byte>()
            });
    }
}
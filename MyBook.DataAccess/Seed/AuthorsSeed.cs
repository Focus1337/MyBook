using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateAuthors(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new
            {
                //https://mybook.ru/author/antoniya-bajett/
                Id = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                FullName = "Антония Сьюзен Байетт",
                Description =
                    " английская писательница. Автор более двух десятков книг, носитель множества почётных учёных степеней различных университетов и лауреат многочисленных литературных наград и премий.",
                Image = Array.Empty<byte>()
            },
            new
            {
                //https://mybook.ru/author/antoniya-bajett/
                Id = new Guid("51e7d2f1-d989-4e59-86c8-278123f564ea"),
                FullName = "Джен Синсеро",
                Description = "Американская писательница, оратор и тренер по успеху.",
                Image = Array.Empty<byte>()
            },
            new
            {
                //https://mybook.ru/author/dzhordzh-oruell/
                Id = new Guid("2ee0cdd2-a3d6-414f-9038-874b12916a86"),
                FullName = "Джордж Оруэлл",
                Description =
                    "Джордж Оруэлл (George Orwell) – творческий псевдоним английского писателя и публициста. Настоящее имя – Эрик Артур Блэр (Eric Arthur Blair). Родился 25 июня 1903 года в Индии в семье британского торгового агента. Оруэлл учился в школе св. Киприана. В 1917 году получил именную стипендию и до 1921 года посещал Итонский Колледж. Жил в Великобритании и других странах Европы, где перебивался случайными заработками и начал писать. Пять лет служил в колониальной полиции в Бирме, про что в 1934 году рассказал в повести «Дни в Бирме».",
                Image = Array.Empty<byte>()
            },
            new
            {
                //https://mybook.ru/author/mark-menson/
                Id = new Guid("02788b50-5eae-42ce-a375-c0416840d687"),
                FullName = "Марк Мэнсон",
                Description =
                    "Американский автор и консультант по личному развитию, предприниматель и блогер. Ведет блог под своим именем на одноименном сайте. По состоянию на 2019 год написал три книги. Книга «Тонкое искусство пофигизма» заняла шестое место в списке бестселлеров The New York Times.",
                Image = Array.Empty<byte>()
            });
    }
}
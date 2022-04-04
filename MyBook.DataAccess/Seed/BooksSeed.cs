using Microsoft.EntityFrameworkCore;
using MyBook.Entity;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateBooks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new
            {
                //https://mybook.ru/author/antoniya-bajett/ragnaryok-2/
                Id = new Guid("3cb92c37-ec67-4720-af23-d7f4d4096109"),
                Title = "Рагнарёк",
                AuthorId = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                Description =
                    "«Рагнарёк» – книга из серии древних мифов, переосмысленных современными писателями из разных стран",
                Genre = "Мифы",
                Year = 2022,
                Price = 549m,
                SubType = 1,
                Image = Array.Empty<byte>()
            },
            new
            {
                //https://mybook.ru/author/antoniya-bajett/obladat/
                Id = new Guid("16d8568d-ec3f-4ee4-9823-514d2c4daf17"),
                Title = "Обладать",
                AuthorId = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                Description =
                    "«Обладать» – один из лучших английских романов конца XX века и, несомненно, лучшее произведение Антонии Байетт. Впрочем, слово «роман» можно применить к этой удивительной прозе весьма условно. Что же такое перед нами? Детективный роман идей? Женский готический роман в современном исполнении? Рыцарский роман на новый лад? Все вместе – и нечто большее, глубоко современная вещь, вобравшая многие традиции и одновременно отмеченная печатью подлинного вдохновения и новаторства. В ней разными гранями переливается тайна английского духа и английского величия.",
                Genre = "Мифы",
                Year = 2016,
                Price = 549m,
                SubType = 1,
                Image = Array.Empty<byte>()
            });
    }
}
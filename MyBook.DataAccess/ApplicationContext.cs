using Microsoft.EntityFrameworkCore;

namespace MyBook.DataAccess;


//dotnet ef migrations add [Название комита] -s .\MyBook\ -p .\MyBook.DataAccess\
//dotnet ef database update
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options)
        : base(options) { }
}
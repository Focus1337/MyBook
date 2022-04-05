using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess.Seed;
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

        Seeds.CreateSubs(modelBuilder);
        Seeds.CreateUsers(modelBuilder);
        Seeds.CreateAuthors(modelBuilder);
        Seeds.CreateBooks(modelBuilder);
        Seeds.CreateBookUser(modelBuilder);
    }
}
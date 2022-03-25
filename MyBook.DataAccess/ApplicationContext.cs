using Microsoft.EntityFrameworkCore;

namespace MyBook.DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options)
        : base(options) { }
}
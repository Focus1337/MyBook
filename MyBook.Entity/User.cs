using Microsoft.AspNetCore.Identity;

namespace MyBook.Entity;

public class User : IdentityUser
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Subscription Sub { get; set; } = null!;
    public DateTime SubDateStart { get; set; }
    public byte[] Image { get; set; } = null!;
    public List<Book> FavoriteBooks { get; set; } = null!;
}
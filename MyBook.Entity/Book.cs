namespace MyBook.Entity;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Author Author { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public decimal Price { get; set; }
    public int SubType { get; set; } //
    public byte[] Image { get; set; } = null!;
    public int Year { get; set; } //
    public List<User> Users { get; set; } = null!;
}
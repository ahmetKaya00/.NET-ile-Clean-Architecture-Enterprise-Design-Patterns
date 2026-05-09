using CleanArchitectureDemo.Domain.Common;

namespace CleanArchitectureDemo.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    private Category() { }
    public static Category Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Kategori adı  boş olamaz.", nameof(name));
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreatedDate = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Kategori adı  boş olamaz.", nameof(name));
        Name = name;
        Description = description;
        LastModifiedDate = DateTime.UtcNow;
    }
}
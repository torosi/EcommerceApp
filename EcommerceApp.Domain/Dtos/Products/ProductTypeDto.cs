using System;

namespace EcommerceApp.Domain.Dtos.Products;

public class ProductTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

}

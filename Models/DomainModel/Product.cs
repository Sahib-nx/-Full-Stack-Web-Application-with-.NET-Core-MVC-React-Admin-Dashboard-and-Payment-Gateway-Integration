using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Models.DomainModel;
using CRM.Models.JunctionModel;
using CRM.Types;

namespace CRM.Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public required string ProductProfileUrl { get; set; }
    public required int Qty { get; set; }
    public int Price { get; set; }
    public required string Category { get; set; }
    public required string SubCategory { get; set; }
    public required string HashTags { get; set; }
    public int SeoScore {get; set;}
    public bool IsArchived { get; set; }
    public bool IsDeleted { get; set; }

    public required Guid SellerId { get; set; }
    [ForeignKey("SellerId")]
    public User? Seller { get; set; }

    public ICollection<CartProduct> Carts { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];


    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;


}

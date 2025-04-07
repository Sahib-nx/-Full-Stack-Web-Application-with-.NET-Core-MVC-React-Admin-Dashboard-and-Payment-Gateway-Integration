using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models.DomainModel;

public class Review
{
    [Key]
    public Guid ReviewId {get; set;} = Guid.NewGuid();
    public required string ReviewText { get; set; }
    public required int Rating { get; set; }

    public required Guid ProductId {get; set;}
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public required Guid UserId {get; set;}
    [ForeignKey("UserId")]
    public User? User { get; set; }

}

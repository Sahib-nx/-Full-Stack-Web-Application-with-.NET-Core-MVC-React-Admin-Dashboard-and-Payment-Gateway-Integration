using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Models.JunctionModel;

namespace CRM.Models;

public class Cart
{
    [Key]
    public Guid CartId {get; set;} = Guid.NewGuid();
    public Guid BuyerId {get; set;}       //Every Cart belongs to a specific user
    [ForeignKey("BuyerId")]
    public User? Buyer {get; set;}
   // public int Qty {get; set;}
    public int CartValue {get; set;}
    //public ICollection<Product> Products {get; set;} = [];

    public ICollection<CartProduct> Products {get; set;} =[];
}

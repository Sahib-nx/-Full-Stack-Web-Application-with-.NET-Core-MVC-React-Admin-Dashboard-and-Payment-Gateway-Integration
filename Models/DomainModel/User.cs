using System;
using System.ComponentModel.DataAnnotations;
using CRM.Models.DomainModel;
using CRM.Types;

namespace CRM.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string UserName { get; set; }
    [EmailAddress]
    public required string Email {get; set;}
    public required string Password {get; set;}
    public required Role Role{get; set;}
    public string? phoneNumber {get; set;}
    public string? Address {get; set;}
    public bool IsActive {get; set;}
    public string? ProfilePicUrl {get; set;}
    public Cart? Cart {get; set;}
    public ICollection<Order> Orders {get; set;} = []; 
    public ICollection<Product> Products {get; set;} = [];
    public ICollection<Address> Addresses {get; set;} =[];
    public ICollection<Review> Reviews {get; set;} = [];

      public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
     public DateTime? DateModified { get; set; }  = DateTime.UtcNow;

}

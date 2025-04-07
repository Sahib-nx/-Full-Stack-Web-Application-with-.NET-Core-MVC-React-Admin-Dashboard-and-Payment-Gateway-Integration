using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Types;

namespace CRM.Models;

public class Order
{
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();
    public required OrderStatus OrderStatus {get; set;}

    public required int OrderPrice {get; set;}
    public required Guid BuyerId {get; set;}
    [ForeignKey("BuyerId")]
    public User? Buyer {get; set;}
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime DispatchDate {get; set;}
    public DateTime ReturnDate { get; set; }
    public bool IsRefunded {get; set;}
    public DateTime CancelDate {get; set;}
    public DateTime? DateModified {get; set;} = DateTime.UtcNow;


}

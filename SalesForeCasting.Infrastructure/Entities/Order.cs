using System.ComponentModel.DataAnnotations;

namespace SalesForeCasting.Infrastructure.Entities;

public class Order
{
    
    public string OrderId { get; set; }

    public DateTime ShipDate { get; set; }

    public string ShipMode { get; set; }

    public string CustomerId { get; set; }

    public string CustomerName { get; set; }

    public string Segment { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string? PostalCode { get; set; }

    public string Region { get; set; }

    public List<Product> Products { get; set; }

    public List<Return> Returns { get; set; }
    
}


// public enum ShipMode
// {
//     StandardClass,
//     FirstClass,
//     SecondClass,
//     SameDay
// }
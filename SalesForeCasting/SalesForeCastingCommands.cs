using System.Globalization;
using CLAP;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SalesForeCasting.Infrastructure;
using SalesForeCasting.Infrastructure.Entities;

namespace SalesForeCasting;

public class SalesForeCastingCommands
{
    private readonly SalesForeCastingDbContext _salesForeCastingDbContext = new();

    /// <summary>
    /// Upload the forecasting data
    /// </summary>
    /// <param name="filePath"></param>
    [Verb]
    public void UploadForeCastingData([Required] string filePath)
    {
        Console.WriteLine(filePath);
    }

    /// <summary>
    /// Get forecasting data for the specified year
    /// </summary>
    /// <param name="year"></param>
    [Verb]
    public void GetForeCasting([Required] int year)
    {
        var products = GetTotalSalesForYear(year);
        
        var profit   = products.Sum(x => x.Profit);

        //TODO: Display data in state wise.
        // var productsGroupedByState = products.GroupBy(x => x.Order.State);
        //
        // foreach (var product in productsGroupedByState)
        // {
        //     Console.WriteLine(product.AsQueryable());
        // }
        
        Console.WriteLine($"Profit for the year {year} is {profit}");
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="percentage"></param>
    /// <param name="state"></param>
    /// <param name="download"></param>
    [Verb]
    public void ApplyPercentage([Required] int year,  [Required] decimal percentage, [Required] string state, bool download = false)
    {
        var products = GetTotalSalesForYear(year).Where(x => x.Order.State == state).ToList();
        
        var profit = (products.Sum(x => x.Profit) * percentage) / 100;
        
        Console.WriteLine($"Profit projected for the year {year} is {profit}");
        
        //TODO Display in State wise...

        if (!download) return;
        
        using var writer = new StreamWriter( "ForeCast.csv");
        using var csv    = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(products);
    }

    private IQueryable<Product> GetTotalSalesForYear(int year)
    {
        var refundOrders = _salesForeCastingDbContext.Returns.Include(x => x.Order)
           .Where(x => x.Order.ShipDate.Year == year).Select(x => x.Order);
       
        var products = from o in _salesForeCastingDbContext.Orders
                       join p in _salesForeCastingDbContext.Products on o.OrderId equals p.OrderId
                       where  o.ShipDate.Year == year && refundOrders.Any(x => x != o)
                       select p;

        return products;
    }
}
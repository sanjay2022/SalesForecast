namespace SalesForeCasting.Infrastructure.Entities;

public class Return
{
    public long      Id        { get; set; }
    public string    OrderId  { get; set; }
    public string Comments { get; set; }
    public Order  Order    { get; set; }
    
}
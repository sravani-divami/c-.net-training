// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System; //for core classes
using System.Collections.Generic; // for collections
using System.Linq;
using Microsoft.VisualBasic; //for special methods

// Expected Output:
//   - A C# console app that:
//   - Filters completed orders
//   - Calculates total revenue
//   - Finds first failed order using FirstOrDefault


// task2
// **Scenario:**

// - Simulate a discount engine.

// - **Rules:**

//   - VIP customers → 10% discount
//   - Order value > 50k → extra 5%
//   - Use nullable types (decimal?)
//   - Avoid null reference exceptions

public enum OrderStatus
{
    completed,
    pending,
    failed
}

public enum CustomerType
{
    regular,
    vip
}
class Order
{
    //define class shape
    public int Id;
    public int Amount;
    public OrderStatus Status;
    public CustomerType CustomerType;
    public decimal Discount;
}

// internal access modifier by defualt
public class Program
{
    static void Main()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, Amount = 100, Status = OrderStatus.completed, CustomerType = CustomerType.regular},
            new Order { Id = 2, Amount = 50, Status = OrderStatus.failed, CustomerType = CustomerType.vip},
            new Order { Id = 3, Amount = 90000, Status = OrderStatus.completed, CustomerType = CustomerType.regular }
        };

        var completedOrders = orders
            .Where(o => o.Status == OrderStatus.completed)
            .ToList();

        var totalRevenue = completedOrders
            .Select(o => o.Amount)
            .Sum();

        var firstFailedOrder = orders
            .FirstOrDefault(o => o.Status == OrderStatus.failed);

        Console.WriteLine($"Completed Orders: {completedOrders.Count}");
        Console.WriteLine($"Total Revenue: {totalRevenue}");
        Console.WriteLine($"First Failed Order Id: {firstFailedOrder?.Id}");
    }
}
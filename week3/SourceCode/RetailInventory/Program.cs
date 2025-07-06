using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        // 1. Retrieve All Products
        var products = await context.Products.ToListAsync();
        Console.WriteLine("All products:");
        foreach (var p in products)
        {
            Console.WriteLine($"{p.Name} - ₹{p.Price}");
        }

        // 2. Find by ID
        var product = await context.Products.FindAsync(1);
        Console.WriteLine($"\nFound product by ID 1: {product?.Name}");

        // 3. FirstOrDefault with Condition
        var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
        Console.WriteLine($"Expensive product (Price > 50,000): {expensive?.Name}");
    }
}

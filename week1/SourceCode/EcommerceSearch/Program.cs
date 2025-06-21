using System.Diagnostics;
class Program
{
    static void Main()
    {
        Product[] products = new Product[]
        {
            new Product(101, "Laptop", "Electronics"),
            new Product(102, "Shirt", "Clothing"),
            new Product(103, "Book", "Stationery"),
            new Product(104, "Mouse", "Electronics"),
            new Product(105, "Notebook", "Stationery")
        };

        // 🔍 Linear Search
        Console.WriteLine("Linear Search for 'Book':");
        var stopwatch1 = Stopwatch.StartNew();
        var result1 = LinearSearch(products, "Book");
        stopwatch1.Stop();
        Console.WriteLine(result1?.ProductName ?? "Not Found");
        Console.WriteLine($"Linear search time: {stopwatch1.Elapsed.TotalMilliseconds} ms\n");

        // 🔍 Binary Search (requires sorted array)
        Array.Sort(products, (a, b) => a.ProductName.CompareTo(b.ProductName));

        Console.WriteLine("Binary Search for 'Book':");
        var stopwatch2 = Stopwatch.StartNew();
        var result2 = BinarySearch(products, "Book");
        stopwatch2.Stop();
        Console.WriteLine(result2?.ProductName ?? "Not Found");
        Console.WriteLine($"Binary search time: {stopwatch2.Elapsed.TotalMilliseconds} ms\n");
    }

    static Product? LinearSearch(Product[] products, string name)
    {
        foreach (var p in products)
        {
            if (p.ProductName == name)
                return p;
        }
        return null;
    }

    static Product? BinarySearch(Product[] products, string name)
    {
        int left = 0, right = products.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int cmp = string.Compare(products[mid].ProductName, name);
            if (cmp == 0) return products[mid];
            else if (cmp < 0) left = mid + 1;
            else right = mid - 1;
        }

        return null;
    }
}

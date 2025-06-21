class Program
{
    static void Main()
    {
        double initialValue = 1000; // starting money
        double growthRate = 0.10;   // 10% growth rate
        int years = 5;
        Console.WriteLine($"Value after {years} years: {FutureValue(initialValue, growthRate, years):C2}");
    }
    // Recursive: FV = PV * (1 + r)^n
    static double FutureValue(double amount, double rate, int years)
    {
        if (years == 0) return amount;
        return FutureValue(amount, rate, years - 1) * (1 + rate);
    }
}

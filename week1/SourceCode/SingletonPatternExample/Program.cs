class Program
{
    static void Main()
    {
        var logger1 = Logger.GetInstance();
        logger1.Log("Hello from logger 1");

        var logger2 = Logger.GetInstance();
        logger2.Log("Hello from logger 2");

        Console.WriteLine(Object.ReferenceEquals(logger1, logger2)
            ? "Same instance"
            : "Different instances");
    }
}

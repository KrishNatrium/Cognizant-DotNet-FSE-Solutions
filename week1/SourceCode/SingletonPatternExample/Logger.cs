public class Logger
{
    private static Logger? _instance;  // static = shared across all uses

    private Logger()                   // private constructor = no one else can create
    {
        Console.WriteLine("Logger created.");
    }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();  // create only if not already created
        }
        return _instance;
    }

    public void Log(string message)
    {
        Console.WriteLine("Log: " + message);
    }
}

namespace ex6;
class Program
{
    public int a;
    private ILog logger;
    public Program(ILog log)
    {
        logger = log;
        logger.uzenetkiir("Most készültem el!");
    }
    static void Main(string[] args)
    {
        //Program program1 = new Program(new TimeMsgLog());
        Program program1 = new Program(new MsgLog());
        program1.a = 5;
        Program program2 = new Program(new TimeMsgLog());
        program2.a = 10;
        Console.WriteLine(program2.a);
        program1.run();
    }

    public void run()
    {

    }
}
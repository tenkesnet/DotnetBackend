namespace ex6;

public class Alkalmazas
{
    
    public List<ILog> ILogList { get; set; }
    //private ILog log;

    public Alkalmazas()
    {
        ILogList = new List<ILog>();  
    }
    public void addVezerlo(ILog a)
    {
        ILogList.Add(a);
    }

    public void display()
    {
        foreach (var a in ILogList)
        {
            a.GetType();

        }
    }

    


    //Console.WriteLine ("valami");
}
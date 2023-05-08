namespace ex6
{
    public class TimeMsgLog : ILog
    {
        public void uzenetkiir(string uzenet)
        {
            DateTime datum = DateTime.Now;
            string date_str = datum.ToString("yyyy.MM.dd");
            Console.WriteLine($"{date_str}: {uzenet}");
        }
    }
}
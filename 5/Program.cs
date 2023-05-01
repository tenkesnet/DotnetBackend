namespace ex5;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        KetDimenzios alakzatok2d = new KetDimenzios();
        alakzatok2d.addAlakzat(new Kor(3));
        alakzatok2d.addAlakzat(new Teglalap(3, 6));
        alakzatok2d.display();
    }
}

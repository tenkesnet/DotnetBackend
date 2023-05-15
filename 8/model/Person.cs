namespace ex8;

public class Person
{
    public String name { get; set; }
    public DateTime birthDate { get; set; }
    public List<String> varosok { get; set; }

    public Address address { get; set; }
    public Person(string name, DateTime birthDate)
    {
        this.name = name;
        this.birthDate = birthDate;
        varosok = new List<string>();
        varosok.Add("Szeged");
        varosok.Add("budapest");
    }
}
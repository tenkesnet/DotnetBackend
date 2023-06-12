using Tanulok.Model;

namespace Tanulok.Service
{
    public class FileDatabase<T> where T:Szemely, new() //Ezt nem értem: T:Tanulo,new() Miért kell generikussá tenni? Mi előnyt jelent 
    {
        private StreamWriter writer { get; set; } //writer változó létrehozása, típusa Streamwriter
        private StreamReader reader { get; set; }  //reader változó létrehozása, típusa Streamreader
        public string fileName { get; set; } 
        public FileDatabase(string filename)
        {  
            fileName= filename;
        }
        public bool write(T altSzemely) 

        {
            string stringObj;
            writer = new StreamWriter(fileName);  //példányosítás
            if (altSzemely is Tanulo)
            {
                Tanulo obj = altSzemely as Tanulo;
                stringObj = $"{obj.Name};{obj.SzulDatum};{obj.Nem};{obj.TanAtlag};{obj.Lakcim.varos};{obj.Lakcim.utca};{obj.Lakcim.hazszam}";
            }
            else
            {
                Tanar obj = altSzemely as Tanar;
                stringObj = $"{obj.Name};{obj.SzulDatum};{obj.Nem};{obj.foTantargy};{obj.Lakcim.varos};{obj.Lakcim.utca};{obj.Lakcim.hazszam}";
            }
            writer.WriteLine(stringObj);  //kiírja stringobj változó tartalmát a megadott fájlba
            writer.Close();  //lezárja a fájlt
            return true;
        }
        public IEnumerable<T> read() //a T az a visszaadott érték típusa, azaz milyen típus? Általános típus? Bármilyen típussal behelyettesíthető?
        {
            List<T> szemelyek = new List<T>();
            string line;
            reader = new StreamReader(fileName);
            line = reader.ReadLine();
            while (line != null)
            {
                T szemely=new T();
                string[] data=line.Split(";");
                szemely.Name = data[0];
                szemely.SzulDatum = DateTime.Parse(data[1]);
                szemely.Nem = data[2];
                if (szemely is Tanulo)
                {
                    (szemely as Tanulo).TanAtlag = Double.Parse(data[3]);
                }
                else
                {
                    (szemely as Tanar).foTantargy = data[3];
                }                
                szemely.Lakcim.varos = data[4]; 
                szemely.Lakcim.utca = data[5];
                szemely.Lakcim.hazszam = Int16.Parse(data[6]);
                szemelyek.Add(szemely);

                line = reader.ReadLine();
            }
            reader.Close();
            return szemelyek;
        }
    }
}

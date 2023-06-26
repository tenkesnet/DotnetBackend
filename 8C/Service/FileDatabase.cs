using Tanulok.Entity;

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
                stringObj = $"{obj.name};{obj.szulDatum};{obj.nem};{obj.tanAtlag}";
            }
            else
            {
                Tanar obj = altSzemely as Tanar;
                stringObj = $"{obj.name} ; {obj.szulDatum} ; {obj.nem} ; {obj.foTantargy}";
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
                szemely.name = data[0];
                szemely.szulDatum = DateTime.Parse(data[1]);
                szemely.nem = data[2];
                if (szemely is Tanulo)
                {
                    (szemely as Tanulo).tanAtlag = Double.Parse(data[3]);
                }
                else
                {
                    (szemely as Tanar).foTantargy = data[3];
                }                
                szemelyek.Add(szemely);

                line = reader.ReadLine();
            }
            reader.Close();
            return szemelyek;
        }
    }
}

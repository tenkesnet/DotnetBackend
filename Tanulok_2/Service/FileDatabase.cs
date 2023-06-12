using Tanulok_2.Model;

namespace Tanulok_2.Service
{
    public class FileDatabase<T> where T : Tanulo, new() //Ezt nem értem: T:Tanulo,new() Miért kell generikussá tenni? Mi előnyt jelent? 
    {
        private StreamWriter writer { get; set; } //writer változó létrehozása, típusa Streamwriter
        private StreamReader reader { get; set; }  //reader változó létrehozása, típusa Streamreader
        public string fileName { get; set; }
        public FileDatabase(string filename)
        {
            fileName = filename;
        }
        public bool write(T obj)
        {
            writer = new StreamWriter(fileName);  //példányosítás
            string stringObj = $"{obj.Nev};{obj.SzulDatum};{obj.Nem};{obj.TanAtlag};{obj.Lakcim}"; //stringObj változó létrehozása és feltöltése a metódusnak átadott objektum Nev ill. SzulDatum tulajdonságával
            writer.WriteLine(stringObj);  //
            writer.Close();
            return true;
        }
        public T read()
        {
            T tanulo = new T();
            string line;
            reader = new StreamReader(fileName);
            line = reader.ReadLine();
            while (line != null)
            {

                string[] data = line.Split(";");
                tanulo.Nev = data[0];
                tanulo.SzulDatum = DateTime.Parse(data[1]);
                line = reader.ReadLine();
            }
            reader.Close();
            return tanulo;
        }
    }
}

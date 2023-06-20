using Microsoft.AspNetCore.Mvc;

namespace Tanulok.Entity;
    public class Lakcim
    {
        public string telepules { get; set; }
        public int irszam { get; set; }
        public string utca { get; set; }
        public int hazszam { get; set; }
        public Lakcim()
        {
            telepules = "";
            utca = "";
        }
    }
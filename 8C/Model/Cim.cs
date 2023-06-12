using Microsoft.AspNetCore.Mvc;

namespace Tanulok.Model;
    public class Cim
    {
        public string varos { get; set; }
        public string utca { get; set; }
        public int hazszam { get; set; }
        public Cim()
        {

        }
    }
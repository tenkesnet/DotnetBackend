namespace Tanulok.DTO
{

    public class AutoSzervizDTO
    {
        public string Rendszam { get; set; } = null!;
        public string TipusNev { get; set; } = null!;
        public long? FutottKm { get; set; }
        public long? SzervizKm { get; set; }
        public int AlkalmazottId { get; set; }
        public string? AlkNev { get; set; }
        public string? Beosztas { get; set; }
    }
}

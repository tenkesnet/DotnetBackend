using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Autok
{
    public int Id { get; set; }

    public string Rendszam { get; set; } = null!;

    public int TipusokId { get; set; }

    public int AutoCsopId { get; set; }

    public DateOnly? VasarlasDatuma { get; set; }

    public double? Ar { get; set; }

    public long? FutottKm { get; set; }

    public long? UtSzerviz { get; set; }

    public string? Allapot { get; set; }

    public int ReszlegId { get; set; }

    public int AlkalmazottId { get; set; }

    public virtual Alkalmazott Alkalmazott { get; set; } = null!;

    public virtual AutoCsop AutoCsop { get; set; } = null!;

    public virtual ICollection<Rendele> Rendeles { get; set; } = new List<Rendele>();

    public virtual Reszleg Reszleg { get; set; } = null!;

    public virtual Tipusok Tipusok { get; set; } = null!;
}

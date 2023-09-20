using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Tipusok
{
    public int Id { get; set; }

    public string TipusNev { get; set; } = null!;

    public int AutoCsopId { get; set; }

    public string? Leiras { get; set; }

    public long? SzervizKm { get; set; }

    public virtual AutoCsop AutoCsop { get; set; } = null!;

    public virtual ICollection<Autok> Autoks { get; set; } = new List<Autok>();
}

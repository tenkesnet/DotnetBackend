using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Ugyfelek
{
    public int Id { get; set; }

    public string UgyfelSzam { get; set; } = null!;

    public string? UgyfelNev { get; set; }

    public string? Cim { get; set; }

    public string? Varos { get; set; }

    public string? Orszag { get; set; }

    public string? IranyitoSzam { get; set; }

    public string? Megbizott { get; set; }

    public string? FizetesiMod { get; set; }

    public virtual ICollection<Rendele> Rendeles { get; set; } = new List<Rendele>();
}

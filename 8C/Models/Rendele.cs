using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Rendele
{
    public int Id { get; set; }

    public string RendelesSzam { get; set; } = null!;

    public int UgyfelekId { get; set; }

    public DateOnly? RendelesDatum { get; set; }

    public string? RendeloSzemely { get; set; }

    public DateOnly? KolcsonKezdete { get; set; }

    public long? Napok { get; set; }

    public int RendszamId { get; set; }

    public int TipusokId { get; set; }

    public long? KmKezdet { get; set; }

    public long? KmVeg { get; set; }

    public long? KolcsonDij { get; set; }

    public string? Fizetes { get; set; }

    public virtual Autok Rendszam { get; set; } = null!;

    public virtual Tipusok Tipusok { get; set; } = null!;

    public virtual Ugyfelek Ugyfelek { get; set; } = null!;
}

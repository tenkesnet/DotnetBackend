﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tanulok.Models;

public partial class Autok
{
    public int Id { get; set; }

    public string Rendszam { get; set; } = null!;

    public int TipusokId { get; set; }

    public DateOnly? VasarlasDatuma { get; set; }

    public double? Ar { get; set; }

    public long? FutottKm { get; set; }

    public long? UtSzerviz { get; set; }

    public string? Allapot { get; set; }

    public int AlkalmazottId { get; set; }
    [JsonIgnore]
    public virtual Alkalmazott Alkalmazott { get; set; } = null!;

    public virtual ICollection<Rendele> Rendeles { get; set; } = new List<Rendele>();

    public virtual Tipusok Tipusok { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Reszleg
{
    public int Id { get; set; }

    public long ReszlegKod { get; set; }

    public string? ReszlegNev { get; set; }

    public string? ReszlegCim { get; set; }

    public virtual ICollection<Alkalmazott> Alkalmazotts { get; set; } = new List<Alkalmazott>();
}

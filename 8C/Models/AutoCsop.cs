using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class AutoCsop
{
    public int Id { get; set; }

    public string AutoCsopNev { get; set; } = null!;

    public int? KmDij { get; set; }

    public long? NapiDij { get; set; }

    public virtual ICollection<Tipusok> Tipusoks { get; set; } = new List<Tipusok>();
}

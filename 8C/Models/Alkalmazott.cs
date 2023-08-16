using System;
using System.Collections.Generic;

namespace Tanulok.Models;

public partial class Alkalmazott
{
    public int Id { get; set; }

    public long AlkKod { get; set; }

    public string? AlkNev { get; set; }

    public string? Beosztas { get; set; }

    public DateOnly? Belepes { get; set; }

    public long? Fizetes { get; set; }

    public long? Premium { get; set; }

    public int ReszlegId { get; set; }

    public virtual ICollection<Autok> Autoks { get; set; } = new List<Autok>();

    public virtual Reszleg Reszleg { get; set; } = null!;
}

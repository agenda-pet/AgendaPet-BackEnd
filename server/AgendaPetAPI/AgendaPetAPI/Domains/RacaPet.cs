using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class RacaPet
{
    public Guid RacaID { get; set; }

    public string NomeRaca { get; set; } = null!;

    public virtual ICollection<Pet> Pet { get; set; } = new List<Pet>();
}

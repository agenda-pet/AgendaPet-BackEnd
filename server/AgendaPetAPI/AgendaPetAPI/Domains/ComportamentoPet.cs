using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class ComportamentoPet
{
    public Guid ComportamentoID { get; set; }

    public string NomeComportamento { get; set; } = null!;

    public virtual ICollection<Pet> Pet { get; set; } = new List<Pet>();
}

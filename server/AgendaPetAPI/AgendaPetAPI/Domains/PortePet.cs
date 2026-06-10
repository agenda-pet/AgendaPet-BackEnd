using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class PortePet
{
    public Guid PorteID { get; set; }

    public string NomePorte { get; set; } = null!;

    public virtual ICollection<Pet> Pet { get; set; } = new List<Pet>();
}

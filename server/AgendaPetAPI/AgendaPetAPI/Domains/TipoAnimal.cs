using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class TipoAnimal
{
    public Guid TipoAnimalID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Pet> Pet { get; set; } = new List<Pet>();
}

using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class Pet
{
    public Guid PetID { get; set; }

    public string Nome { get; set; } = null!;

    public Guid TipoAnimalID { get; set; }

    public Guid ComportamentoID { get; set; }

    public Guid RacaID { get; set; }

    public Guid PorteID { get; set; }

    public Guid UsuarioID { get; set; }

    public virtual ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();

    public virtual ComportamentoPet Comportamento { get; set; } = null!;

    public virtual PortePet Porte { get; set; } = null!;

    public virtual RacaPet Raca { get; set; } = null!;

    public virtual TipoAnimal TipoAnimal { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}

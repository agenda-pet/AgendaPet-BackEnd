using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class Usuario
{
    public Guid UsuarioID { get; set; }

    public string Nome { get; set; } = null!;

    public string NumeroTelefone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? Senha { get; set; }

    public Guid TipoUsuarioID { get; set; }

    public Guid StatusUsuarioID { get; set; }

    public virtual ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();

    public virtual ICollection<Pet> Pet { get; set; } = new List<Pet>();

    public virtual StatusUsuario StatusUsuario { get; set; } = null!;

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class StatusUsuario
{
    public Guid StatusUsuarioID { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}

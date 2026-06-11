using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class StatusAgendamento
{
    public Guid StatusAgendamentoID { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();
}

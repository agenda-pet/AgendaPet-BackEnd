using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class AgendamentoServico
{
    public Guid ServicoID { get; set; }

    public Guid AgendamentoID { get; set; }

    public int TempoPorServico { get; set; }

    public virtual Agendamento Agendamento { get; set; } = null!;

    public virtual Servico Servico { get; set; } = null!;
}

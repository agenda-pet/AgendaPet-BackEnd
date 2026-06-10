using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class LogAgendamento
{
    public Guid LogAgendamentoID { get; set; }

    public DateTime? DataModificacao { get; set; }

    public DateTime DataAnteriorAgendameto { get; set; }

    public string StatusAgendamentoAnterior { get; set; } = null!;

    public string ServicosPorAgendamento { get; set; } = null!;

    public Guid AgendamentoID { get; set; }

    public virtual Agendamento Agendamento { get; set; } = null!;
}

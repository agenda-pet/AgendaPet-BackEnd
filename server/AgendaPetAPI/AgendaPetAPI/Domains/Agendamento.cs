using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class Agendamento
{
    public Guid AgendamentoID { get; set; }

    public DateOnly DataAgendamento { get; set; }

    public TimeOnly HoraAgendamento { get; set; }

    public decimal ValorTotal { get; set; }

    public Guid StatusAgendamentoID { get; set; }

    public Guid FuncionarioID { get; set; }

    public Guid PetID { get; set; }

    public int TempoTotal { get; set; }

    public virtual Usuario Funcionario { get; set; } = null!;

    public virtual ICollection<LogAgendamento> LogAgendamento { get; set; } = new List<LogAgendamento>();

    public virtual Pet Pet { get; set; } = null!;

    public virtual StatusAgendamento StatusAgendamento { get; set; } = null!;

    public virtual ICollection<Servico> Servico { get; set; } = new List<Servico>();
}

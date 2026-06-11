using System;
using System.Collections.Generic;

namespace AgendaPetAPI.Domains;

public partial class Servico
{
    public Guid ServicoID { get; set; }

    public string NomeServico { get; set; } = null!;

    public decimal Preco { get; set; }

    public int TempoServico { get; set; }

    public virtual ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();
}

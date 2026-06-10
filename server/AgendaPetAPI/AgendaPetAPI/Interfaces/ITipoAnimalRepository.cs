using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface ITipoAnimalRepository
    {
        public List<TipoAnimal> Listar();
        public TipoAnimal ObterPorId(int id);
    }
}

using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IPetRepository
    {
        public List<Pet> Listar();
        public Pet ObterPorId(Guid id);
        public List<Pet> ObterPorTutor(Guid tutorId);
        public void Adicionar(Pet pet);
        public void Atualizar(Guid petId, Pet pet);
        public void Remover(Guid id);
    }
}

using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IPetRepository
    {
        public List<Pet> Listar();
        public Pet ObterPorId(Guid id);
        public Pet ObterPorTutor(Guid tutorId, Guid petId);
        public void Adicionar(Pet pet);
        public void Atualizar(Guid tutorId, Pet pet);
        public void Remover(Guid id);
    }
}

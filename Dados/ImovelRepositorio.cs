using Dados.Models;

namespace Dados
{
    public interface IImovelRepositorio
    {
        public List<Imovel> GetAll();
        Imovel Add(Imovel imovel);
        object GetById(Guid id);
        void Remove(object imovelExcluido);
    }
}

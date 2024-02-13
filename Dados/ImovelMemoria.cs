using Dados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ImovelMemoria : IImovelRepositorio
    {
        private List<Imovel> _imoveis = new List<Imovel>
        {
            new Imovel
            {
                Rua = "Augusta",
                Numero = 1010,
                Complemento = "Apto 505",
                Descricao = "Sem descrição",
                Proprietario = "Zaha Haddid"
            }
        };

        public Imovel Add(Imovel imovel)
        {
            _imoveis.Add(imovel);
            return imovel;
        }

        public List<Imovel> GetAll()
        {
            return _imoveis;
        }
    }

    
}

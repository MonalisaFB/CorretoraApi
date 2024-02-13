namespace Dados.Models
{
    public class Imovel
    {
        public Guid Id { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Descricao { get; set; }
        public string Proprietario { get; set; }

    }

}

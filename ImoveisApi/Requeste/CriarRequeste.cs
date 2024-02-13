namespace ImoveisApi.Requeste
{
    public record CriarRequeste(string Rua, int Numero, string Complemento, string Descricao, string Cep, string Proprietario)
    {
        public CriarRequeste(string rua, int numero, string complemento, string descricao, string cep, string proprietario, string endereco) : this(rua, numero, complemento, descricao, cep, proprietario)
        {
            Endereco = endereco;
        }

        public string Endereco { get; internal set; }
    }
}

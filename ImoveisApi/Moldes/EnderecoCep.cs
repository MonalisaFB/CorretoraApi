namespace ImoveisApi.Moldes
{
    public class EnderecoCep
    {
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Bairro { get; set; }
        public string Rua {  get; set; }
        public int Numero { get; set; }


        public string EscreverEndereco()
        {
            return $"{this.Rua} {this.Numero}, {this.Bairro} - {this.Cidade}/{this.Estado}";
        }
    }
}

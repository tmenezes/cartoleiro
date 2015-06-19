namespace Cartoleiro.Crawler
{
    public class CrawlingInfo
    {
        public int TotalDeObjetos { get; set; }
        public int ObjetosCarregados { get; set; }
        public string DadosDoObjeto { get; set; }

        public CrawlingInfo(int totalDeObjetos, int objetosCarregados, string dadosDoObjeto)
        {
            TotalDeObjetos = totalDeObjetos;
            ObjetosCarregados = objetosCarregados;
            DadosDoObjeto = dadosDoObjeto;
        }
    }
}
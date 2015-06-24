namespace Cartoleiro.Crawler
{
    public class CrawlingInfo
    {
        public int TotalDeObjetos { get; set; }
        public int ObjetosCarregados { get; set; }
        public string TipoDoObjeto { get; set; }
        public string DadosDoObjeto { get; set; }

        public CrawlingInfo(int totalDeObjetos, int objetosCarregados, object objetoCarregado)
        {
            TotalDeObjetos = totalDeObjetos;
            ObjetosCarregados = objetosCarregados;
            TipoDoObjeto = objetoCarregado.GetType().Name;
            DadosDoObjeto = objetoCarregado.ToString();
        }
    }
}
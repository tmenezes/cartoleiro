using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Extensions;
using Newtonsoft.Json;

namespace Cartoleiro.DAO
{
    public class CartolaJsonDataSource : ICartolaDataSource
    {
        private readonly string _pastaAppData;
        private string ArquivoClubes { get { return Path.Combine(_pastaAppData, "clubes.json"); } }
        private string ArquivoJogadores { get { return Path.Combine(_pastaAppData, "jogadores.json"); } }
        private string ArquivoRodadas { get { return Path.Combine(_pastaAppData, "rodadas.json"); } }
        private string ArquivoHistoricoDeJogos { get { return Path.Combine(_pastaAppData, "historicoDeJogos.json"); } }

        public IEnumerable<Clube> Clubes { get; private set; }
        public IEnumerable<Jogador> Jogadores { get; private set; }
        public IEnumerable<Rodada> Rodadas { get; private set; }
        public IEnumerable<Jogo> HistoricoDeJogos { get; private set; }


        public CartolaJsonDataSource(string pastaAppData)
        {
            _pastaAppData = pastaAppData;

            PopularClubes();
            PopularJogadores();
            PopularRodadas();
            PopularHistoricoDeJogos();

            CartolaData.Iniciar(this);
        }


        private void PopularClubes()
        {
            Clubes = GetObjetos<Clube>(ArquivoClubes);

            foreach (var clube in Clubes)
            {
                clube.Campeonato.SetClube(clube);
            }
        }

        private void PopularJogadores()
        {
            Jogadores = GetObjetos<Jogador>(ArquivoJogadores);

            // executa ajustes no datasource json
            Jogadores.SetClubes(Clubes);
        }

        private void PopularRodadas()
        {
            Rodadas = GetObjetos<Rodada>(ArquivoRodadas);
            Campeonato.Rodadas = new Rodadas(Rodadas);

            foreach (var rodada in Rodadas)
            {
                foreach (var jogo in rodada.Jogos)
                {
                    jogo.Mandante = Clubes.FirstOrDefault(c => c.Nome == jogo.Mandante.Nome);
                    jogo.Visitante = Clubes.FirstOrDefault(c => c.Nome == jogo.Visitante.Nome);
                }
            }
        }

        private void PopularHistoricoDeJogos()
        {
            HistoricoDeJogos = GetObjetos<Jogo>(ArquivoHistoricoDeJogos);

            foreach (var jogo in HistoricoDeJogos)
            {
                var clubeMandate = Clubes.FirstOrDefault(c => string.Equals(c.Nome, jogo.Mandante.Nome, StringComparison.CurrentCultureIgnoreCase));
                var clubeVisitante = Clubes.FirstOrDefault(c => string.Equals(c.Nome, jogo.Visitante.Nome, StringComparison.CurrentCultureIgnoreCase));

                if (clubeMandate != null)
                    jogo.Mandante = clubeMandate;

                if (clubeVisitante != null)
                    jogo.Visitante = clubeVisitante;
            }
        }


        private IEnumerable<T> GetObjetos<T>(string arquivo)
        {
            var objectos = new List<T>();

            using (var reader = new StreamReader(arquivo, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    var item = JsonConvert.DeserializeObject<T>(reader.ReadLine());
                    objectos.Add(item);
                }
            }

            return objectos;
        }
    }
}
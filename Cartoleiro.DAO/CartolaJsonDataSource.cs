using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Newtonsoft.Json;

namespace Cartoleiro.DAO
{
    public class CartolaJsonDataSource : ICartolaDataSource
    {
        private readonly string _pastaAppData;
        private string ArquivoClubes { get { return Path.Combine(_pastaAppData, "clubes.json"); } }
        private string ArquivoJogadores { get { return Path.Combine(_pastaAppData, "jogadores.json"); } }

        public IEnumerable<Clube> Clubes { get; private set; }
        public IEnumerable<Jogador> Jogadores { get; private set; }


        public CartolaJsonDataSource(string pastaAppData)
        {
            _pastaAppData = pastaAppData;

            PopularClubes();
            PopularJogadores();
        }


        private void PopularClubes()
        {
            Clubes = GetObjetos<Clube>(ArquivoClubes);
        }

        private void PopularJogadores()
        {
            Jogadores = GetObjetos<Jogador>(ArquivoJogadores);

            // executa ajustes no datasource json
            foreach (var jogador in Jogadores)
            {
                var clube = Clubes.FirstOrDefault(c => c.Nome == jogador.Clube.Nome);

                if (clube != null)
                    jogador.Clube = clube;

                if (jogador.Scouts == null)
                    jogador.Scouts = new Scouts();
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
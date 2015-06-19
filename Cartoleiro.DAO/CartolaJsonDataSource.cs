using System;
using System.Collections.Generic;
using System.IO;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Newtonsoft.Json;

namespace Cartoleiro.DAO
{
    public class CartolaJsonDataSource : ICartolaDataSource
    {
        private readonly string _pastaAppData;
        private string CaminhoDataSource { get { return Path.Combine(_pastaAppData, "jogadores.json"); } }

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
            Clubes = new List<Clube>();
        }

        private void PopularJogadores()
        {
            var jogadores = new List<Jogador>();

            using (var reader = new StreamReader(CaminhoDataSource))
            {
                while (!reader.EndOfStream)
                {
                    var jogador = JsonConvert.DeserializeObject<Jogador>(reader.ReadLine());
                    jogadores.Add(jogador);
                }

            }

            Jogadores = jogadores;
        }
    }
}
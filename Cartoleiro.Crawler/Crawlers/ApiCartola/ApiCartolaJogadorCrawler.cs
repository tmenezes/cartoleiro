using System;
using System.Globalization;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola.Json;
using Posicao = Cartoleiro.Core.Cartola.Posicao;

namespace Cartoleiro.Crawler.Crawlers.ApiCartola
{
    public class ApiCartolaJogadorCrawler
    {
        private static readonly CultureInfo _cultura = CultureInfo.GetCultureInfo("en-US");


        public static Jogador ObterJogador(Atleta atleta)
        {
            var id = atleta.Id;
            var nome = atleta.Apelido.Trim();
            var clube = CrawlerHelper.GetClube(atleta.Clube.Nome);

            var posicao = GetPosicao(atleta.Posicao.Nome);
            var status = GetStatus(atleta.Status);
            var scouts = GetScouts(atleta);

            var jogador = new Jogador(nome, clube, posicao)
                          {
                              Id = id,
                              Preco = GetPreco(atleta),
                              Pontuacao = GetPontuacao(atleta),
                              Jogos = GetQuantidadeDeJogos(atleta),
                              Status = status,
                              Scouts = scouts,
                          };

            return jogador;
        }


        private static Preco GetPreco(Atleta atleta)
        {
            var precoAtual = Convert.ToDouble(atleta.Preco, _cultura);
            var variacao = Convert.ToDouble(atleta.Variacao, _cultura);

            return new Preco(precoAtual, variacao);
        }

        private static Pontuacao GetPontuacao(Atleta atleta)
        {
            var pontuacaoMedia = Convert.ToDouble(atleta.Media, _cultura);
            var pontuacaoAtual = Convert.ToDouble(atleta.Pontos, _cultura);

            return new Pontuacao(pontuacaoMedia, pontuacaoAtual);
        }

        private static int GetQuantidadeDeJogos(Atleta atleta)
        {
            return Convert.ToInt32(atleta.Jogos);
        }

        private static Posicao GetPosicao(string posicao)
        {
            if (posicao.ToLower() == "meia")
            {
                return Posicao.MeioCampo;
            }

            if (posicao.ToLower() == "lateral")
            {
                return Posicao.Lateral;
            }

            if (posicao.ToLower() == "goleiro")
            {
                return Posicao.Goleiro;
            }

            if (posicao.ToLower() == "atacante")
            {
                return Posicao.Atacante;
            }

            if (posicao.ToLower() == "zagueiro")
            {
                return Posicao.Zagueiro;
            }

            if (posicao.ToLower() == "técnico")
            {
                return Posicao.Tecnico;
            }

            return Posicao.MeioCampo;
        }

        private static Status GetStatus(string status)
        {
            if (status.ToLower() == "barrado")
            {
                return Status.Barrado;
            }

            if (status.ToLower() == "contundido")
            {
                return Status.Contundido;
            }

            if (status.ToLower() == "dúvida")
            {
                return Status.Duvida;
            }

            if (status.ToLower() == "provável")
            {
                return Status.Provavel;
            }

            if (status.ToLower() == "suspenso")
            {
                return Status.Suspenso;
            }

            if (status.ToLower() == "vendido")
            {
                return Status.Vendido;
            }

            if (status.ToLower() == "nulo")
            {
                return Status.Desconhecido;
            }

            return Status.Provavel;
        }

        private static Scouts GetScouts(Atleta atleta)
        {
            var scouts = new Scouts();

            foreach (var scout in atleta.Scout)
            {
                scouts.SetScout(scout.Nome, scout.Quantidade);
            }

            return scouts;
        }
    }
}
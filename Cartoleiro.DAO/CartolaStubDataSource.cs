using System;
using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Cartoleiro.DAO
{
    public class CartolaStubDataSource : ICartolaDataSource
    {
        public IEnumerable<Clube> Clubes { get; private set; }
        public IEnumerable<Jogador> Jogadores { get; private set; }
        public IEnumerable<Rodada> Rodadas { get; private set; }

        public CartolaStubDataSource()
        {
            PopularClubes();
            PopularJogadores();

            CartolaData.Iniciar(this);
        }


        private void PopularClubes()
        {
            Fixture fixture = new Fixture();

            Clubes = fixture.CreateMany<Clube>(20);
        }

        private void PopularJogadores()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 10)));
            fixture.Customizations.Add(new RandomDoubleSequenceGenerator(0, 15));

            var jogadores = new List<Jogador>();

            // goleiros
            jogadores.AddRange(fixture.Build<Jogador>()
                                      .With(j => j.Posicao, Posicao.Goleiro)
                                      .CreateMany(20));

            // laterais
            jogadores.AddRange(fixture.Build<Jogador>()
                                      .With(j => j.Posicao, Posicao.Lateral)
                                      .CreateMany(40));

            // zagueiros
            jogadores.AddRange(fixture.Build<Jogador>()
                          .With(j => j.Posicao, Posicao.Zagueiro)
                          .CreateMany(40));

            // meiocampos
            jogadores.AddRange(fixture.Build<Jogador>()
                          .With(j => j.Posicao, Posicao.MeioCampo)
                          .CreateMany(80));

            // atacantes
            jogadores.AddRange(fixture.Build<Jogador>()
                          .With(j => j.Posicao, Posicao.Atacante)
                          .CreateMany(40));

            // tecnicos
            jogadores.AddRange(fixture.Build<Jogador>()
                                      .With(j => j.Posicao, Posicao.Tecnico)
                                      .CreateMany(20));

            Jogadores = jogadores;
        }

    }

    internal class RandomDoubleSequenceGenerator : ISpecimenBuilder
    {
        private readonly int _min;
        private readonly int _max;
        private readonly object syncRoot;
        private readonly Random random;

        internal RandomDoubleSequenceGenerator(int min, int max)
        {
            _min = min;
            _max = max;

            this.syncRoot = new object();
            this.random = new Random();
        }

        public object Create(object request, ISpecimenContext context)
        {
            var type = request as Type;
            if (type == null)
                return new NoSpecimen(request);

            return this.CreateRandom(type);
        }

        private double GetNextRandom()
        {
            lock (this.syncRoot)
            {
                return random.NextDouble() * random.Next(_min, _max);
            }
        }

        private object CreateRandom(Type request)
        {
            switch (Type.GetTypeCode(request))
            {
                case TypeCode.Decimal:
                    return (decimal)
                        GetNextRandom();

                case TypeCode.Double:
                    return (double)
                        GetNextRandom();

                case TypeCode.Single:
                    return (float)
                        GetNextRandom();

                default:
                    return new NoSpecimen(request);
            }
        }
    }
}
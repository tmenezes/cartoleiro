using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    public static class EsquemaTaticoHelper
    {
        private const int ZAGA = 0;
        private const int MEIO_CAMPO = 1;
        private const int ATAQUE = 2;
        private const int TOTAL_LATERAIS = 2;

        private static readonly Dictionary<EsquemaTatico, int[]> _jogadoresPorEsquema;

        static EsquemaTaticoHelper()
        {
            _jogadoresPorEsquema = new Dictionary<EsquemaTatico, int[]>()
                                   {
                                       {EsquemaTatico._343, new[]{ 3,4,3 }},
                                       {EsquemaTatico._352, new[]{ 3,5,2 }},
                                       {EsquemaTatico._433, new[]{ 4,3,3 }},
                                       {EsquemaTatico._442, new[]{ 4,4,2 }},
                                       {EsquemaTatico._451, new[]{ 4,5,1 }},
                                       {EsquemaTatico._532, new[]{ 5,3,2 }},
                                       {EsquemaTatico._541, new[]{ 5,4,1 }},
                                   };
        }


        // publicos
        public static int GetNumeroTotalDeJogadores(Posicao posicao, EsquemaTatico esquema)
        {
            switch (posicao)
            {
                case Posicao.Lateral:
                    return NumeroDeLaterais(esquema);

                case Posicao.Zagueiro:
                    return NumeroTotalDaZaga(esquema);

                case Posicao.MeioCampo:
                    return NumeroTotalDoMeioCampo(esquema);

                case Posicao.Atacante:
                    return NumeroTotalDoAtaque(esquema);

                default:
                    return 1;
            }
        }

        public static int GetNumeroDeJogadores(Posicao posicao, EsquemaTatico esquema)
        {
            switch (posicao)
            {
                case Posicao.Lateral:
                    return NumeroDeLaterais(esquema);

                case Posicao.Zagueiro:
                    return NumeroDeZagueiros(esquema);

                case Posicao.MeioCampo:
                    return NumeroDeMeioCampos(esquema);

                case Posicao.Atacante:
                    return NumeroDeAtacantes(esquema);

                default:
                    return 1;
            }
        }


        public static int NumeroDeZagueiros(EsquemaTatico esquema)
        {
            int totalDaZaga = GetTotalDeJogadores(esquema, ZAGA);
            int totalDeLaterais = NumeroDeLaterais(esquema);

            return totalDaZaga - totalDeLaterais;
        }

        public static int NumeroDeLaterais(EsquemaTatico esquema)
        {
            return NumeroTotalDaZaga(esquema) > 3 ? TOTAL_LATERAIS : 0;
        }

        public static int NumeroDeMeioCampos(EsquemaTatico esquema)
        {
            return GetTotalDeJogadores(esquema, MEIO_CAMPO);
        }

        public static int NumeroDeAtacantes(EsquemaTatico esquema)
        {
            return GetTotalDeJogadores(esquema, ATAQUE);
        }


        public static int NumeroTotalDaZaga(EsquemaTatico esquema)
        {
            return GetTotalDeJogadores(esquema, ZAGA);
        }

        public static int NumeroTotalDoMeioCampo(EsquemaTatico esquema)
        {
            return GetTotalDeJogadores(esquema, MEIO_CAMPO);
        }

        public static int NumeroTotalDoAtaque(EsquemaTatico esquema)
        {
            return GetTotalDeJogadores(esquema, ATAQUE);
        }


        public static bool PossuiLaterais(Posicao posicao, EsquemaTatico esquema)
        {
            bool zagaComLaterais = posicao == Posicao.Zagueiro && NumeroTotalDaZaga(esquema) >= NumeroTotalDoMeioCampo(esquema);
            bool meioComLaterais = posicao == Posicao.MeioCampo && NumeroTotalDoMeioCampo(esquema) > NumeroTotalDaZaga(esquema);

            return zagaComLaterais || meioComLaterais;
        }


        private static int GetTotalDeJogadores(EsquemaTatico esquema, int setorDoCampo)
        {
            if (!_jogadoresPorEsquema.ContainsKey(esquema))
                return 0;

            return _jogadoresPorEsquema[esquema][setorDoCampo];
        }
    }
}

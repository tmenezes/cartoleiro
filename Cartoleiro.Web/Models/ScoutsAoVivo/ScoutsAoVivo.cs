﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Cartoleiro.Web.Models.ScoutsAoVivo
{
    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("abv")]
        public string Abv { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("playerlist")]
        public Playerlist Playerlist { get; set; }

        public string GetUrlImagem()
        {
            var nomeSemAcento = ModelUtils.RemoverAcentos(Name.ToLower().Replace(" ", ""));
            var imagem = string.Format("~/Images/clubes/{0}.png", nomeSemAcento);

            return UrlHelper.GenerateContentUrl(imagem, HttpContext.Current.Request.RequestContext.HttpContext);
        }

        public string GetPlacar()
        {
            return !string.IsNullOrEmpty(Score)
                ? Score
                : "0";
        }
    }

    public class Match
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("home")]
        public Team Home { get; set; }

        [JsonProperty("away")]
        public Team Away { get; set; }
    }

    public class FixtureMatches
    {
        [JsonProperty("fixture")]
        public string Fixture { get; set; }

        [JsonProperty("matches")]
        public IList<Match> Matches { get; set; }
    }

    public class Player
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("classposition")]
        public string Classposition { get; set; }

        [JsonProperty("scouts")]
        public string Scouts { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("ycard")]
        public string Ycard { get; set; }

        [JsonProperty("goals")]
        public string Goals { get; set; }

        public string GetCorLabelTotal()
        {
            if (Total < 0)
                return "label-danger";

            if (Total >= 5)
                return "label-success";

            return "label-default";
        }
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("sub")]
        public string Sub { get; set; }
    }

    public class Playerlist
    {
        [JsonProperty("formation")]
        public string Formation { get; set; }

        [JsonProperty("items")]
        public IList<Item> Items { get; set; }
    }

    public class ScoutsMatch
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("home")]
        public Team Home { get; set; }

        [JsonProperty("away")]
        public Team Away { get; set; }
    }

    public class ScoutsData
    {
        [JsonProperty("fixtureMatches")]
        public FixtureMatches FixtureMatches { get; set; }

        [JsonProperty("scoutsMatch")]
        public ScoutsMatch ScoutsMatch { get; set; }
    }
}

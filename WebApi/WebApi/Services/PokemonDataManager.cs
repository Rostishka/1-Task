using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Abstract;
using WebApi.Models;

namespace WebApi.Implementation
{
    public class PokemonDataManager : IDataManager
    {
        public IEnumerable<Pokemon> GetPokemons()
        {
            return new List<Pokemon>
            {
                new Pokemon {Id = 0, Name = "Pikachu"},
                new Pokemon {Id = 1, Name = "Charmander"},
                new Pokemon {Id = 2, Name = "Charizard"},
            };
        }
    }
}
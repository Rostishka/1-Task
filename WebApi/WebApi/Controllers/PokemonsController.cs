using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Abstract;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PokemonsController : ApiController
    {
        // GET: api/Pokemons
        public PokemonsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // GET: api/Pokemons
        public IEnumerable<Pokemon> Get()
        {
            return _dataManager.GetPokemons();
        }

        // GET: api/Pokemons/5
        public IHttpActionResult Get(int id)
        {
            if (id < 0 || id > _dataManager.GetPokemons().Count())
            {
                return BadRequest();
            }
            return Json(_dataManager.GetPokemons().FirstOrDefault(x => x.Id == id));
        }

        // POST: api/Pokemons
        public HttpResponseMessage Post(Int32 id,[FromBody]string pokemonName)
        {
            if (id < _dataManager.GetPokemons().Count() || String.IsNullOrWhiteSpace(pokemonName))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (_dataManager.GetPokemons().FirstOrDefault(x => x.Id == id) != null)
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent("There is pokemon with such Id")
                };
                return response;
            }

            _dataManager.GetPokemons().ToList().Add(new Pokemon
            {
                Id = id,
                Name = pokemonName
            });

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT: api/Pokemons/5
        public IHttpActionResult Put([FromBody]Pokemon pokemon)
        {
            if (pokemon.Id < 0 || pokemon.Id >= _dataManager.GetPokemons().Count())
            {
                return BadRequest();
            }
            var listPokemon = _dataManager.GetPokemons().FirstOrDefault(x => x.Id == pokemon.Id);
            if (listPokemon == null)
            {
                return InternalServerError();
            }
            listPokemon.Name = pokemon.Name;
            return Ok();
        }

        // DELETE: api/Pokemons/5
        public IHttpActionResult Delete(int id)
        {
            IEnumerable<String> headerValues = Request.Headers.GetValues("Authorization");
            var token = headerValues.FirstOrDefault();
            if (token == "1234")
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Unauthorized();
        }
        
        private readonly IDataManager _dataManager;
    }
}

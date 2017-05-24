using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Abstract
{
    public interface IDataManager
    {
        IEnumerable<Pokemon> GetPokemons();
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class FriendsController : ApiController
    {
        ApplicationContext db = new ApplicationContext();

        public IEnumerable<Friend> Get()
        {
            return db.Friends.ToList();
        }

        public Friend Get(int id)
        {
            return db.Friends.Find(id);
        }

        public IHttpActionResult Post([FromBody]Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Friends.Add(friend);
            db.SaveChanges();
            return Ok(friend);
        }

        public IHttpActionResult Put([FromBody]Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(friend).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(friend);
        }

        public IHttpActionResult Delete(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend != null)
            {
                db.Friends.Remove(friend);
                db.SaveChanges();
                return Ok(friend);
            }
            return NotFound();
        }
    }
}
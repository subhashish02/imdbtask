using System;
using System.Collections.Generic;
using System.Web.Http;
using imdb.Models;
using imdb.Utility;

namespace imdb.Controllers
{

    public class ActorController : ApiController
    {
        // GET: api/Actors
        public List<Actor> Get()
        {
            return ActorUtility.GetActors();
        }

        // GET: api/Actors/5
        public Actor Get(int id)
        {
            return ActorUtility.GetActor(id);
        }

        // POST: api/Actors
        public BaseResponse Post(Actor value)
        {
            BaseResponse br = ActorUtility.SaveActor(value);
            return br;
        }

        // PUT: api/Actors/5
        public BaseResponse Put(int id, Actor value)
        {
            return ActorUtility.UpdateActor(id, value);
        }

        // DELETE: api/Actors/5
        public BaseResponse Delete(int id)
        {
        return ActorUtility.DeleteActor(id);
        }
    }
}

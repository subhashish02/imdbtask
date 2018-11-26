using System;
using System.Collections.Generic;
using System.Web.Http;
using imdb.Models;
using imdb.Utility;

namespace imdb.Controllers
{

    public class ProducerController : ApiController
    {
        // GET: api/Producers
        public List<Producer> Get()
        {
            return ProducerUtility.GetProducers();
        }

        // GET: api/Producers/5
        public Producer Get(int id)
        {
            return ProducerUtility.GetProducer(id);
        }

        // POST: api/Producers
        public BaseResponse Post(Producer value)
        {
            BaseResponse br = ProducerUtility.SaveProducer(value);
            return br;
        }

        // PUT: api/Producers/5
        public BaseResponse Put(int id, Producer value)
        {
            return ProducerUtility.UpdateProducer(id, value);
        }

        // DELETE: api/Producers/5
        public BaseResponse Delete(int id)
        {
        return ProducerUtility.DeleteProducer(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web.Http;
using imdb.Models;
using imdb.Utility;

namespace imdb.Controllers
{

    public class MovieController : ApiController
    {
        // GET: api/Movies
        public List<Movie> Get()
        {
            return MovieUtility.GetMovies();
        }

        // GET: api/Movies/5
        public Movie Get(int id)
        {
            return MovieUtility.GetMovie(id);
        }

        // POST: api/Movies
        public BaseResponse Post(Movie value)
        {
            BaseResponse br = MovieUtility.SaveMovie(value);
            return br;
        }

        // PUT: api/Movies/5
        public BaseResponse Put(int id, Movie value)
        {
            return MovieUtility.UpdateMovie(id, value);
        }

        // DELETE: api/Movies/5
        public BaseResponse Delete(int id)
        {
        return MovieUtility.DeleteMovie(id);
        }
    }
}

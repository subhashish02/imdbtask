using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class Movie
    {
       public  long movid {get;set;}
       public  long proid {get;set;}
       public string moviname {get;set;}
       public int movirelyear {get;set;}
       public string moviplot {get;set;}
       public string moviposter {get;set;}
        public string posterImg { get; set; }
        public Producer producer { get; set; }
        public List<Actor> actors { get; set; }
    }
}
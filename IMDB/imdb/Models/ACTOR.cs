using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class Actor
    {
       public  int actid {get;set;}
       public string actname {get;set;}
       public string actsex {get;set;}
       public DateTime? actdob {get;set;}
       public string actbio {get;set;}

    }
}
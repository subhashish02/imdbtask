using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class BaseResponse
    {
        public String status { get; set; }
        public String message { get; set; }
        public long lid { get; set; }
    }
}
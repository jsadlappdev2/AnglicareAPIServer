using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnglicareAPIServer.Models
{
    public class todos
    {

        public int id { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public bool isDone { get; set; }

    }
}
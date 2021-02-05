using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeAPI.Models
{
    public class todosViewModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string completed { get; set; }
    }
}
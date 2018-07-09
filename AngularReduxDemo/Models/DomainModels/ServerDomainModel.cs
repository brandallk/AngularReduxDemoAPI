using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.DomainModels
{
    public class ServerDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TableCount { get; set; }
    }
}
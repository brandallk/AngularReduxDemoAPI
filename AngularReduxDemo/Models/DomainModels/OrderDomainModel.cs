using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.DomainModels
{
    public class OrderDomainModel
    {
        public int Id { get; set; }
        public int? TableId { get; set; }
        public int? ServerId { get; set; }
    }
}
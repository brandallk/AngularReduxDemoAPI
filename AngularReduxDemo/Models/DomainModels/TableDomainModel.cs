using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.DomainModels
{
    public class TableDomainModel
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? ServerId { get; set; }
    }
}
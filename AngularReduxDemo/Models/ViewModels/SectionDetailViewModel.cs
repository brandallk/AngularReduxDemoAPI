using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.ViewModels
{
    public class SectionDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TableCount { get; set; }
        public List<TableDetailViewModel> Tables { get; set; }
    }
}
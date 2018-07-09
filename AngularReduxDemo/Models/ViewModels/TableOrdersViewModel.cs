﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.ViewModels
{
    public class TableOrdersViewModel
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? ServerId { get; set; }
        public string ServerName { get; set; }
        public List<OrderDetailViewModel> Orders { get; set; }
    }
}
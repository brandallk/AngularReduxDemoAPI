using AngularReduxDemo.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularReduxDemo.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int? TableId { get; set; }
        public int? ServerId { get; set; }
        public List<OrderItemDomainModel> MealItems { get; set; }
        public List<OrderItemDomainModel> DrinkItems { get; set; }
    }
}
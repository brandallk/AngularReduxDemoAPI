using AngularReduxDemo.EF;
using AngularReduxDemo.Models.DomainModels;
using AngularReduxDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularReduxDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Tables")]
    public class TablesController : ApiController
    {
        [HttpGet, Route("{id}/Orders")]
        // GET api/Tables/{id}/Orders
        public TableOrdersViewModel GetTableOrders(int id)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                Table table = context.Tables.FirstOrDefault(x => x.Id == id);
                Server server = context.Servers.FirstOrDefault(x => x.Id == table.ServerId);
                List<Order> orders = context.Orders.Where(x => x.TableId == id).ToList();
                List<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
                foreach (Order order in orders)
                {
                    List<MealItem> mealItems = context.MealItems.Where(x => x.OrderId == order.Id).ToList();
                    List<OrderItemDomainModel> mealItemsDetails = new List<OrderItemDomainModel>();
                    foreach (MealItem item in mealItems)
                    {
                        var mealItemDetail = new OrderItemDomainModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            OrderId = item.OrderId
                        };
                        mealItemsDetails.Add(mealItemDetail);
                    }

                    List<DrinkItem> drinkItems = context.DrinkItems.Where(x => x.OrderId == order.Id).ToList();
                    List<OrderItemDomainModel> drinkItemsDetails = new List<OrderItemDomainModel>();
                    foreach (DrinkItem item in drinkItems)
                    {
                        var drinkItemDetail = new OrderItemDomainModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            OrderId = item.OrderId
                        };
                        drinkItemsDetails.Add(drinkItemDetail);
                    }

                    var orderDetail = new OrderDetailViewModel
                    {
                        Id = order.Id,
                        TableId = order.TableId,
                        ServerId = order.ServerId,
                        MealItems = mealItemsDetails,
                        DrinkItems = drinkItemsDetails
                    };
                    orderDetails.Add(orderDetail);
                }

                var tableOrdersDetails = new TableOrdersViewModel
                {
                    Id = table.Id,
                    SectionId = table.SectionId,
                    ServerId = table.ServerId,
                    ServerName = server.Name,
                    Orders = orderDetails
                };

                return tableOrdersDetails;
            }
        }

        [HttpPost, Route("")]
        // POST api/Tables
        public HttpResponseMessage PostTable([FromBody] TableDomainModel table)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    Table newTable = new Table
                    {
                        SectionId = table.SectionId,
                        ServerId = table.ServerId
                    };
                    context.Tables.Add(newTable);
                    context.SaveChanges();
                    return response;
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpDelete, Route("{id}")]
        // DELETE api/Tables/{id}
        public HttpResponseMessage DeleteTable(int id)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    var table = context.Tables.FirstOrDefault(x => x.Id == id);

                    var tableOrders = context.Orders.Where(x => x.TableId == id).ToList();
                    foreach (var order in tableOrders)
                    {
                        var mealItems = context.MealItems.Where(x => x.OrderId == order.Id).ToList();
                        context.MealItems.RemoveRange(mealItems);
                        context.SaveChanges();

                        var drinkItems = context.DrinkItems.Where(x => x.OrderId == order.Id).ToList();
                        context.DrinkItems.RemoveRange(drinkItems);
                        context.SaveChanges();
                    }
                    context.Orders.RemoveRange(tableOrders);
                    context.SaveChanges();

                    context.Tables.Remove(table);
                    context.SaveChanges();

                    return response;
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
        }


    }
}

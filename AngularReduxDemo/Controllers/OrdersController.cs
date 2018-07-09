using AngularReduxDemo.EF;
using AngularReduxDemo.Models.DomainModels;
using AngularReduxDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularReduxDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {       

        [HttpPost, Route("")]
        // POST api/Orders
        public async Task<HttpResponseMessage> PostOrder([FromBody] OrderDetailViewModel order)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    Order newOrder = new Order
                    {
                        TableId = order.TableId,
                        ServerId = order.ServerId
                    };
                    context.Orders.Add(newOrder);
                    await Task.Run(() => context.SaveChanges());

                    List<MealItem> mealItems = new List<MealItem>();
                    foreach (OrderItemDomainModel item in order.MealItems)
                    {
                        var mealItem = new MealItem
                        {
                            Name = item.Name,
                            OrderId = newOrder.Id
                        };
                        mealItems.Add(mealItem);
                    }
                    context.MealItems.AddRange(mealItems);
                    context.SaveChanges();

                    List<DrinkItem> drinkItems = new List<DrinkItem>();
                    foreach (OrderItemDomainModel item in order.DrinkItems)
                    {
                        var drinkItem = new DrinkItem
                        {
                            Name = item.Name,
                            OrderId = newOrder.Id
                        };
                        drinkItems.Add(drinkItem);
                    }
                    context.DrinkItems.AddRange(drinkItems);
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
        // DELETE api/Orders/{id}
        public HttpResponseMessage DeleteOrder(int id)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    var order = context.Orders.FirstOrDefault(x => x.Id == id);
                    var mealItems = context.MealItems.Where(x => x.OrderId == id).ToList();
                    var drinkItems = context.DrinkItems.Where(x => x.OrderId == id).ToList();

                    context.MealItems.RemoveRange(mealItems);
                    context.SaveChanges();

                    context.DrinkItems.RemoveRange(drinkItems);
                    context.SaveChanges();

                    context.Orders.Remove(order);
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

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
    [RoutePrefix("api/Servers")]
    public class ServersController : ApiController
    {
        [HttpGet, Route("")]
        // GET api/Servers
        public IEnumerable<ServerDomainModel> GetSections()
        {
            using (var context = new AngularReduxDemoEntities())
            {
                List<Server> servers = context.Servers.Select(x => x).ToList();
                IEnumerable<ServerDomainModel> serverList = new List<ServerDomainModel>();
                serverList = from server in servers
                              select new ServerDomainModel
                              {
                                  Id = server.Id,
                                  Name = server.Name,
                                  TableCount = server.TableCount
                              };
                return serverList;
            }
        }

        [HttpPatch, Route("{id}/Update")]
        // PUT api/Servers/{id}/Update
        public HttpResponseMessage UpdateServer(int id, [FromBody] CountChange tableCountChange)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    var server = context.Servers.FirstOrDefault(x => x.Id == id);
                    server.TableCount += Convert.ToInt32(tableCountChange.change);
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

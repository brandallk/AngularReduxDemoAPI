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
    [RoutePrefix("api/Sections")]
    public class SectionsController : ApiController
    {
        //[HttpGet, Route("")]
        //// GET api/Sections
        //public IEnumerable<SectionDomainModel> GetSections()
        //{
        //    using (var context = new AngularReduxDemoEntities())
        //    {
        //        List<Section> sections = context.Sections.Select(x => x).ToList();
        //        IEnumerable<SectionDomainModel> sectionList = new List<SectionDomainModel>();
        //        sectionList = from section in sections
        //                      select new SectionDomainModel
        //                   {
        //                       Id = section.Id,
        //                       Name = section.Name,
        //                       TableCount = section.TableCount
        //                   };
        //        return sectionList;
        //    }
        //}

        [HttpGet, Route("")]
        // GET api/Sections
        public IEnumerable<SectionDetailViewModel> GetSections()
        {
            using (var context = new AngularReduxDemoEntities())
            {
                List<Section> sections = context.Sections.Select(x => x).ToList();
                List<SectionDetailViewModel> sectionList = new List<SectionDetailViewModel>();

                foreach (var section in sections)
                {
                    List<Table> tables = context.Tables.Where(x => x.SectionId == section.Id).ToList();
                    List<TableDetailViewModel> tableList = new List<TableDetailViewModel>();
                    foreach (var table in tables)
                    {
                        Server server = context.Servers.FirstOrDefault(x => x.Id == table.ServerId);
                        List<Order> orders = context.Orders.Where(x => x.TableId == table.Id).ToList();
                        TableDetailViewModel tableDetail = new TableDetailViewModel
                        {
                            Id = table.Id,
                            SectionId = table.SectionId,
                            ServerId = table.ServerId,
                            ServerName = server.Name,
                            OrderIds = orders.Select(x => x.Id).ToList()
                        };
                        tableList.Add(tableDetail);
                    }
                    SectionDetailViewModel sectionDetail = new SectionDetailViewModel
                    {
                        Id = section.Id,
                        Name = section.Name,
                        TableCount = section.TableCount,
                        Tables = tableList
                    };
                    sectionList.Add(sectionDetail);
                }

                return sectionList;
            }
        }

        [HttpPatch, Route("{id}/Update")]
        // PUT api/Sections/{id}/Update
        public HttpResponseMessage UpdateSection(int id, [FromBody] CountChange tableCountChange)
        {
            using (var context = new AngularReduxDemoEntities())
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                try
                {
                    var section = context.Sections.FirstOrDefault(x => x.Id == id);
                    section.TableCount += Convert.ToInt32(tableCountChange.change);
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

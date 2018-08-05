using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KanbanBackend.Models;
using KanbanBackend.Repository;

namespace KanbanBackend.Controllers
{
    [RoutePrefix("api/items")]
    public class itemsController : ApiController
    {
        private itemsRepo itRepo = new itemsRepo();

        // GET: api/items/{status} optional status, get all projects only
        [HttpGet]
        [Route("{status?}")]
        public IHttpActionResult GetItems(string status = null)
        {
            try
            {
                return Ok(itRepo.getItems(status));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/items
        [HttpPost]
        public IHttpActionResult PostItem(itemDTO itemInput)
        {
            try
            {
                itRepo.createItem(itemInput);
                return Ok("Item created");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/items/update for item updates
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateItem(acronymedItemDTO itemInput)
        {
            try
            {
                itRepo.updateItem(itemInput);
                return Ok("Item updated");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
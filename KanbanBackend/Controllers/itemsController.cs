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

        // GET: api/items
        public IHttpActionResult Getitems()
        {
            try
            {
                return Ok(itRepo.getItems());
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/items/status
        [Route("{status}")]
        public IHttpActionResult Getitems(string status)
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
        public IHttpActionResult Postitem(itemDTO itemInput)
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
        [Route("update")]
        public IHttpActionResult Updateitem(acronymedItemDTO itemInput)
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

        //POST: api/items/update for status updates only
        [Route("status-update")]
        public IHttpActionResult Updateitem(statusedItemDTO itemInput)
        {
            try
            {
                itRepo.updateItemStatus(itemInput);
                return Ok("Item status updated");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
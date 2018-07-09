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

namespace KanbanBackend.Controllers
{
    [RoutePrefix("api/items")]
    public class itemsController : ApiController
    {
        private KanbanDBEntities db = new KanbanDBEntities();

        // GET: api/items
        public IQueryable<acronymedItem> Getitems()
        {
            return db.items.ToList().Join(db.projects, i => i.p_id, p => p.id, (it,pr)=> new acronymedItem
            {
                id = it.id,
                type = it.type,
                priority = it.priority,
                title = it.title,
                description = it.description,
                status = it.status,
                acronym = pr.acronym
            }).AsQueryable();

        }

        // GET: api/items/status
        [Route("{status}")]
        public IQueryable<acronymedItem> Getitems(string status)
        {
            return db.items.ToList().Where(i => i.status == status).Join(db.projects, i => i.p_id, p => p.id, (it, pr) => new acronymedItem
            {
                id = it.id,
                type = it.type,
                priority = it.priority,
                title = it.title,
                description = it.description,
                status = it.status,
                acronym = pr.acronym
            }).AsQueryable();


        }

        // POST: api/items
        [ResponseType(typeof(item))]
        public async Task<IHttpActionResult> Postitem(item itemInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item i = new item();
            i.id = itemInput.id;
            i.p_id = itemInput.p_id;
            i.type = itemInput.type;
            i.priority = itemInput.priority;
            i.title = itemInput.title;
            i.description = itemInput.description;
            i.status = itemInput.status;

            db.items.Add(i);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = i.id }, i);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool itemExists(int id)
        {
            return db.items.Count(e => e.id == id) > 0;
        }
    }
}
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
        public List<acronymedItem> Getitems()
        {
            return db.items.Join(db.projects, i => i.p_id, p => p.id, (it,pr)=> new acronymedItem
            {
                id = it.id,
                type = it.type,
                priority = it.priority,
                title = it.title,
                description = it.description,
                status = it.status,
                acronym = pr.acronym
            }).ToList();

        }

        // GET: api/items/status
        [Route("{status}")]
        public List<acronymedItem> Getitems(string status)
        {
            return db.items.Where(i => i.status == status).Join(db.projects, i => i.p_id, p => p.id, (it, pr) => new acronymedItem
            {
                id = it.id,
                type = it.type,
                priority = it.priority,
                title = it.title,
                description = it.description,
                status = it.status,
                acronym = pr.acronym
            }).ToList();


        }

        // POST: api/items
        [ResponseType(typeof(item))]
        public IHttpActionResult Postitem(item itemInput)
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
            db.SaveChangesAsync();

            return Ok("Item created");
        }

        // POST: api/items/update for item updates
        [Route("update")]
        public IHttpActionResult Updateitem(acronymedItem itemInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item i = db.items.Find(itemInput.id);
            i.type = itemInput.type;
            i.priority = itemInput.priority;
            i.description = itemInput.description;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (i.id != itemInput.id)
            {
                return BadRequest();
            }

            db.Entry(i).State = EntityState.Modified;

            try
            {
                db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemExists(itemInput.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Item updated");
        }

        //POST: api/items/update for status updates only
        [Route("status-update")]
        public IHttpActionResult Updateitem(statusedItem itemInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item i = db.items.Find(itemInput.id);
            i.status = itemInput.status;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (i.id != itemInput.id)
            {
                return BadRequest();
            }

            db.Entry(i).State = EntityState.Modified;

            try
            {
                db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemExists(itemInput.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Item status updated");
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
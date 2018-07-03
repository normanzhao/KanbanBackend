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
    public class itemsController : ApiController
    {
        private KanbanDBEntities db = new KanbanDBEntities();

        // GET: api/items
        public IQueryable<item> Getitems()
        {
            return db.items.ToList()
            .Select(i => new item
            {
                id = i.id,
                p_id = i.p_id,
                type = i.type,
                priority = i.priority,
                title = i.title,
                description = i.description
            }).AsQueryable();
        }

        // GET: api/items/5
        [ResponseType(typeof(item))]
        public async Task<IHttpActionResult> Getitem(int id)
        {
            item item = await db.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putitem(int id, item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/items
        [ResponseType(typeof(item))]
        public async Task<IHttpActionResult> Postitem(item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.items.Add(item);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = item.id }, item);
        }

        // DELETE: api/items/5
        [ResponseType(typeof(item))]
        public async Task<IHttpActionResult> Deleteitem(int id)
        {
            item item = await db.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            db.items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
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
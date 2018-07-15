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
    [RoutePrefix("api/projects")]
    public class projectsController : ApiController
    {
        private KanbanDBEntities db = new KanbanDBEntities();

        // GET: api/projects
        public IQueryable<project> Getprojects()
        {
            return db.projects.ToList()
            .Select(p => new project
            {
                id = p.id,
                acronym = p.acronym,
                title = p.title,
                description = p.description,
                status = p.status
            }).AsQueryable();
        }

        // GET: api/projects/status
        [Route("{status}")]
        [ResponseType(typeof(project))]
        public IQueryable<project> Getprojects(string status)
        {
            return db.projects.ToList().Where(p => p.status == status)
            .Select(p => new project
            {
                id = p.id,
                acronym = p.acronym,
                title = p.title,
                description = p.description,
                status = p.status
            }).AsQueryable();
        }

        // POST: api/projects
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Postproject(project projectInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project p = new project();
            p.id = projectInput.id;
            p.acronym = projectInput.acronym.ToUpper();
            p.title = projectInput.title;
            p.description = projectInput.description;
            p.status = projectInput.status;


            db.projects.Add(p);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = p.id }, p);
        }

        // POST: api/projects/update
        [Route("update")]
        public async Task<IHttpActionResult> Updateproject(statusedProject projectInput)
        {
            System.Diagnostics.Debug.Write("\n\n\n\nasdasdasdasd\n\n");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project p = db.projects.Find(projectInput.id);
            p.status = projectInput.status;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (p.id != projectInput.id)
            {
                return BadRequest();
            }

            db.Entry(p).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectExists(projectInput.id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool projectExists(int id)
        {
            return db.projects.Count(e => e.id == id) > 0;
        }
    }
}
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
                description = p.description
            }).AsQueryable();
        }

        // GET: api/projects/5
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Getproject(int id)
        {
            project project = await db.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/projects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproject(int id, project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.id)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectExists(id))
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
            p.acronym = projectInput.acronym;
            p.title = projectInput.title;
            p.description = projectInput.description;


            db.projects.Add(p);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = p.id }, p);
        }

        // DELETE: api/projects/5
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Deleteproject(int id)
        {
            project project = await db.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            db.projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
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
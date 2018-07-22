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
        public List<project> Getprojects()
        {
            return db.projects
            .Select(p => new project
            {
                id = p.id,
                acronym = p.acronym,
                title = p.title,
                description = p.description,
                status = p.status
            }).ToList();
        }

        // GET: api/projects/status
        [Route("released")]
        [ResponseType(typeof(project))]
        public List<releasedProject> Getreleased()
        {
            return db.projects.Where(p => p.status == "released")
            .Select(p => new releasedProject
            {
                id = p.id,
                acronym = p.acronym,
                title = p.title,
                description = p.description,
                status = p.status,
                items = db.items.ToList().Where(i => i.p_id == p.id).Select(i => new item
                {
                    id = i.id,
                    p_id = i.p_id,
                    type = i.type,
                    priority = i.priority,
                    title = i.title,
                    description = i.description,
                    status = i.status
                }).ToArray()
            }).ToList();
        }

        // GET: api/projects/status
        [Route("{status}")]
        [ResponseType(typeof(project))]
        public List<project> Getprojects(string status)
        {
            return db.projects.Where(p => p.status == status)
            .Select(p => new project
            {
                id = p.id,
                acronym = p.acronym,
                title = p.title,
                description = p.description,
                status = p.status
            }).ToList();
        }

        // POST: api/projects
        [ResponseType(typeof(project))]
        public IHttpActionResult Postproject(project projectInput)
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
            db.SaveChangesAsync();

            return Ok("Project created");
        }

        // POST: api/projects/update
        [Route("update")]
        public IHttpActionResult Updateproject(statusedProject projectInput)
        {

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
            foreach (item i in db.items.ToList().Where(i => i.p_id == projectInput.id))
            {
                i.status = projectInput.status;
            }

            try
            {
                db.SaveChangesAsync();
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

            return Ok("Project updated");
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
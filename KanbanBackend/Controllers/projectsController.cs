using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using KanbanBackend.Models;
using KanbanBackend.Repository;

namespace KanbanBackend.Controllers
{
    [RoutePrefix("api/projects")]
    public class projectsController : ApiController
    {
        private projectsRepo projRepo = new projectsRepo();

        // GET: api/projects/{status} optional status, get all projects only
        [HttpGet]
        [Route("{status?}")]
        public IHttpActionResult GetProjects(string status = null)
        {
            try
            {
                return Ok(projRepo.getProjects(status));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/projects/all/{status} optional status, get all projects alng with their associated items
        [HttpGet]
        [Route("all/{status?}")]
        public IHttpActionResult GetProjectsWithItems(string status = null)
        {
            try
            {
                return Ok(projRepo.getProjectsWithItems(status));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/projects
        [HttpPost]
        public IHttpActionResult Postproject(projectDTO projectInput)
        {
            try
            {
                projRepo.createProject(projectInput);
                return Ok("Project created");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }

        }

        // POST: api/projects/update
        [HttpPost]
        [Route("update")]
        public IHttpActionResult Updateproject(statusedProjectDTO projectInput)
        {
            try
            {
                projRepo.updateProject(projectInput);
                return Ok("Project updated");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
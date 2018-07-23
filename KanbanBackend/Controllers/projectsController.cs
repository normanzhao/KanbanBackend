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

        // GET: api/projects
        public IHttpActionResult Getprojects()
        {
            try
            {
                return Ok(projRepo.getProjects());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/projects/status
        [Route("{status}")]
        public IHttpActionResult Getprojects(string status)
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

        // GET: api/projects/status
        [Route("released")]
        public IHttpActionResult Getreleased()
        {
            try
            {
                return Ok(projRepo.getReleasedProjects());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/projects
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
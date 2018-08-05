using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KanbanBackend.Models;

namespace KanbanBackend.Repository
{
    public class projectsRepo: IprojectsRepo
    {
        private KanbanDBEntities db = new KanbanDBEntities();

        //returns projects with an optional filter for status
        public List<projectDTO> getProjects(string status = null)
        {
            try
            {
                var projects = db.projects.Select(p => new projectDTO
                {
                    id = p.id,
                    acronym = p.acronym,
                    title = p.title,
                    description = p.description,
                    status = p.status
                }).ToList();

                if (status != null)
                {
                    return projects.Where(p => p.status == status).ToList();
                }

                return projects.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //return projects and all associated items, used for history or viewer
        public List<projectWithItemsDTO> getProjectsWithItems(string status)
        {
            try
            {   
                var projects = db.projects
                    .Select(p => new projectWithItemsDTO
                    {
                        id = p.id,
                        acronym = p.acronym,
                        title = p.title,
                        description = p.description,
                        status = p.status,
                        items = db.items.Where(i => i.p_id == p.id).Select(i => new itemDTO
                        {
                            id = i.id,
                            p_id = i.p_id,
                            type = i.type,
                            priority = i.priority,
                            title = i.title,
                            description = i.description,
                            status = i.status
                        })
                    }).ToList();

                if (status != null)
                {
                    return projects.Where(p => p.status == status).ToList();
                }

                return projects;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //create a new project
        public void createProject(projectDTO projectInput)
        {
            try
            {
                project p = new project();
                p.id = projectInput.id;
                p.acronym = projectInput.acronym.ToUpper();
                p.title = projectInput.title;
                p.description = projectInput.description;
                p.status = projectInput.status;

                db.projects.Add(p);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //update project status, project information cannot be changed
        public void updateProject(statusedProjectDTO projectInput)
        {
            try
            {
                project p = db.projects.Find(projectInput.id);
                p.status = projectInput.status;

                foreach (item i in db.items.ToList().Where(i => i.p_id == projectInput.id))
                {
                    i.status = projectInput.status;
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
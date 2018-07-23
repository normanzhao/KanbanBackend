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

        public List<projectDTO> getProjects(string status = null)
        {
            try
            {
                if (status == null)
                {
                    return db.projects
                    .Select(p => new projectDTO
                    {
                        id = p.id,
                        acronym = p.acronym,
                        title = p.title,
                        description = p.description,
                        status = p.status
                    }).ToList();
                }
                else
                {
                    return db.projects.Where(p => p.status == status)
                    .Select(p => new projectDTO
                    {
                        id = p.id,
                        acronym = p.acronym,
                        title = p.title,
                        description = p.description,
                        status = p.status
                    }).ToList();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<projectWithItemsDTO> getProjectsWithItems(string status = "all")
        {
            try
            {   if(status == "all")
                {
                    return db.projects
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
                }
                else
                {
                    return db.projects.Where(p => p.status == status)
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
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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
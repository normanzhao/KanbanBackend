using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KanbanBackend.Models;

namespace KanbanBackend.Repository
{
    public interface IprojectsRepo
    {
        List<projectDTO> getProjects(string status = null);
        List<releasedProjectDTO> getReleasedProjects();
        void createProject(projectDTO projectInput);
        void updateProject(statusedProjectDTO projectInput);
    }
}
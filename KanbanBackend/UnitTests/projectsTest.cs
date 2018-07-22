using KanbanBackend.Controllers;
using KanbanBackend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.Results;


namespace KanbanBackend.UnitTests
{
    [TestClass]
    public class projectsTest
    {
        //helper to assert and compare if every field of 2 projects are equal
        public void assertCompareAcronymedItems(projectDTO item1, projectDTO item2)
        {
            Assert.AreEqual(item1.id, item2.id);
            Assert.AreEqual(item1.acronym, item2.acronym);
            Assert.AreEqual(item1.title, item2.title);
            Assert.AreEqual(item1.description, item2.description);
            Assert.AreEqual(item1.status, item2.status);
        }

        //universal controller
        projectsController controller = new projectsController();

        //Test to see if all projects get returned
        [TestMethod]
        public void ShouldGetAllProjects()
        {
            //Arrange 
            List<projectDTO> expectedResult = new List<projectDTO>();
            expectedResult.Add(new projectDTO
            {
                id = 1,
                acronym = "UI",
                title = "User Interface",
                description = "User interface/front end for the Kanban board using React.",
                status = "released"
                
            });

            //Act
            var result = controller.Getprojects() as OkNegotiatedContentResult<List<projectDTO>>;

            //Assert
            Assert.IsNotNull(result);
            assertCompareAcronymedItems(expectedResult[0], result.Content[0]);
        }

        //Test to see if all projects of a certain status get returned
        [TestMethod]
        public void WhenProvidedStatus_ShouldGetAllProjects()
        {
            //Arrange 
            List<projectDTO> expectedResult = new List<projectDTO>();
            expectedResult.Add(new projectDTO
            {
                id = 1,
                acronym = "UI",
                title = "User Interface",
                description = "User interface/front end for the Kanban board using React.",
                status = "released"

            });

            //Act
            var result = controller.Getprojects("released") as OkNegotiatedContentResult<List<projectDTO>>;

            //Assert
            Assert.IsNotNull(result);
            assertCompareAcronymedItems(expectedResult[0], result.Content[0]);
        }

        //Test to see if a new project is successfully created
        [TestMethod]
        public void CreateNewProject()
        {
            //Arrange
            projectDTO newProject = new projectDTO
            {
                title = "Test Item",
                acronym = "TS",
                description = "This is my a unit test.",
                status = "open",
            };

            //Act
            var result = controller.Postproject(newProject) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Project created", result.Content);
        }

        //Test to see if a project was successully updated
        [TestMethod]
        public void UpdateProject()
        {
            //Arrange
            statusedProjectDTO updatedProject = new statusedProjectDTO
            {
                id = 1,
                status = "open",
            };

            //Act
            var result = controller.Updateproject(updatedProject) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Project updated", result.Content);

            //Cleanup
            updatedProject.status = "released";
            controller.Updateproject(updatedProject);


        }
    }
}
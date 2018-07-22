using KanbanBackend.Controllers;
using KanbanBackend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace KanbanBackendUnitTests
{
    [TestClass]
    public class itemsUnitTest
    {
        [TestMethod]
        public void ShouldGetAllItems()
        {
            //Arrange 
            itemsController controller = new itemsController();
            List<acronymedItem> expectedResult = new List<acronymedItem>();
            expectedResult.Add(new acronymedItem
            {
                id = 1,
                type = "Story",
                priority = 1,
                title = "Header",
                description = "Display a header with buttons that displays modals for different actions. These buttons will be History, Release, New Project and New Item.",
                status = "Released",
                acronym = "UI"
            });

            //Act
            var result = controller.Getitems() as List<acronymedItem>;

            //Assert
            Assert.AreEqual(expectedResult[0], result[0]);
        }
    }
}

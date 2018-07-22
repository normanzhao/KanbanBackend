﻿using KanbanBackend.Controllers;
using KanbanBackend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace KanbanBackendUnitTests
{
    [TestClass]
    public class itemsUnitTest
    {
        //helper to assert and compare if every field of 2 acronymedItems are equal
        public void assertCompareAcronymedItems(acronymedItem item1, acronymedItem item2)
        {
            Assert.AreEqual(item1.id, item2.id);
            Assert.AreEqual(item1.type, item2.type);
            Assert.AreEqual(item1.priority, item2.priority);
            Assert.AreEqual(item1.title, item2.title);
            Assert.AreEqual(item1.description, item2.description);
            Assert.AreEqual(item1.status, item2.status);
            Assert.AreEqual(item1.acronym, item2.acronym);
        }

        //universal controller
        itemsController controller = new itemsController();

        //Test to see if all items get returned
        [TestMethod]
        public void ShouldGetAllItems()
        {
            //Arrange 
            List<acronymedItem> expectedResult = new List<acronymedItem>();
            expectedResult.Add(new acronymedItem
            {
                id = 1,
                type = "Story",
                priority = 1,
                title = "Header",
                description = "Display a header with buttons that displays modals for different actions. These buttons will be History, Release, New Project and New Item.",
                status = "released",
                acronym = "UI"
            });

            //Act
            var result = controller.Getitems() as List<acronymedItem>;

            //Assert
            Assert.IsNotNull(result);
            assertCompareAcronymedItems(expectedResult[0], result[0]);
        }

        //Test to see if all items of a certain status gets returned
        [TestMethod]
        public void WhenProvidedStatus_ShouldGetAllItems()
        {
            //Arrange 
            List<acronymedItem> expectedResult = new List<acronymedItem>();
            expectedResult.Add(new acronymedItem
            {
                id = 1,
                type = "Story",
                priority = 1,
                title = "Header",
                description = "Display a header with buttons that displays modals for different actions. These buttons will be History, Release, New Project and New Item.",
                status = "released",
                acronym = "UI"
            });

            //Act
            var result = controller.Getitems() as List<acronymedItem>;

            //Assert
            Assert.IsNotNull(result);
            assertCompareAcronymedItems(expectedResult[0], result[0]);
        }

        //Test to see if a new item is successfully created
        [TestMethod]
        public void CreateNewItem()
        {
            //Arrange
            item newItem = new item
            {
                p_id = 2,
                type = "Story",
                priority = 1,
                title = "Test Item",
                description = "This is my third unit test.",
                status = "open",
            };

            //Act
            var result = controller.Postitem(newItem) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item created", result.Content);
        }

        //Test to see if an item is successfully updated
        [TestMethod]
        public void UpdateItem()
        {
            //Arrange
            acronymedItem updatedItem = new acronymedItem
            {
                id = 1,
                type = "Feature",
            };

            //Act
            var result = controller.Updateitem(updatedItem) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item updated", result.Content);

            //Cleanup
            updatedItem.type = "Story";
            controller.Updateitem(updatedItem);


        }

        //Test to see if an item's status is successfully updated
        [TestMethod]
        public void UpdateItemStatus()
        {
            //Arrange
            statusedItem updatedItemStatus = new statusedItem
            {
                id = 1,
                status="open"
            };

            //Act
            var result = controller.Updateitem(updatedItemStatus) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item status updated", result.Content);
        }

    }
}

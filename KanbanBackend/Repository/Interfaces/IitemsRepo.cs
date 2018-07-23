using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KanbanBackend.Models;

namespace KanbanBackend.Repository
{
    public interface IitemsRepo
    {
        List<acronymedItemDTO> getItems(string status = null);
        void createItem(itemDTO itemInput);
        void updateItem(acronymedItemDTO itemInput);
        void updateItemStatus(statusedItemDTO itemInput);
    }
}
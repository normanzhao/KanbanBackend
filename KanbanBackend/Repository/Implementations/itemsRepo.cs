using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KanbanBackend.Models;

namespace KanbanBackend.Repository
{
    public class itemsRepo: IitemsRepo
    {
        private KanbanDBEntities db = new KanbanDBEntities();

        //returns items with an optional filter for status
        public List<acronymedItemDTO> getItems(string status = null)
        {
            try
            {
                var items = db.items.Join(db.projects, i => i.p_id, p => p.id, (it, pr) => new acronymedItemDTO
                {
                    id = it.id,
                    type = it.type,
                    priority = it.priority,
                    title = it.title,
                    description = it.description,
                    status = it.status,
                    acronym = pr.acronym
                }).ToList();

                if (status != null)
                {
                    return items.Where(i => i.status == status).ToList();
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //create a new item
        public void createItem(itemDTO itemInput)
        {
            try
            {
                item i = new item();
                i.id = itemInput.id;
                i.p_id = itemInput.p_id;
                i.type = itemInput.type;
                i.priority = itemInput.priority;
                i.title = itemInput.title;
                i.description = itemInput.description;
                i.status = itemInput.status;

                db.items.Add(i);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //update item, an item's informaion and status can't be logically updated at the same time on the front end
        public void updateItem(acronymedItemDTO itemInput)
        {
            try
            {
                item i = db.items.Find(itemInput.id);
                if(i.status == itemInput.status)
                {
                    i.type = itemInput.type;
                    i.priority = itemInput.priority;
                    i.description = itemInput.description;
                }

                i.status = itemInput.status;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
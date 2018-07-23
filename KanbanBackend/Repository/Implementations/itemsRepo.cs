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

        public List<acronymedItemDTO> getItems(string status = null)
        {
            try
            {
                if (status == null)
                {
                    return db.items.Join(db.projects, i => i.p_id, p => p.id, (it, pr) => new acronymedItemDTO
                    {
                        id = it.id,
                        type = it.type,
                        priority = it.priority,
                        title = it.title,
                        description = it.description,
                        status = it.status,
                        acronym = pr.acronym
                    }).ToList();
                }
                else
                {
                    return db.items.Where(i => i.status == status).Join(db.projects, i => i.p_id, p => p.id, (it, pr) => new acronymedItemDTO
                    {
                        id = it.id,
                        type = it.type,
                        priority = it.priority,
                        title = it.title,
                        description = it.description,
                        status = it.status,
                        acronym = pr.acronym
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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

        public void updateItem(acronymedItemDTO itemInput)
        {
            try
            {
                item i = db.items.Find(itemInput.id);
                i.type = itemInput.type;
                i.priority = itemInput.priority;
                i.description = itemInput.description;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void updateItemStatus(statusedItemDTO itemInput)
        {
            try
            {

                item i = db.items.Find(itemInput.id);
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
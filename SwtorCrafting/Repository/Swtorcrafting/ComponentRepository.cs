using SwtorCrafting.Model.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools.Swtorcrafting
{
    public class ComponentRepository : IRepository<Component>
    {
        // Database Tools
        public DataBaseTools dbTools;

        public ComponentRepository()
        {
            dbTools = DataBaseTools.getInstance();
            dbTools.createTableIfNotExist<Component>();
        }
        public Component FindOneById(int id)
        {
            return dbTools.
                FindOneById<Component>(id);
        }

        public List<Component> FindAll()
        {
            return dbTools.FindAll<Component>();
        }

        public void Update(Component element)
        {
            Component currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                currentElement.name = element.name;
                currentElement.componentRankId = element.componentRankId;
                currentElement.componentTypeId = element.componentTypeId;
                currentElement.rarityId = element.rarityId;
                dbTools.Update(currentElement);
            }
        }

        public void Insert(Component element)
        {
            dbTools.Insert(element);
        }

        public void InsertAll(List<Component> elements)
        {
            if (elements != null)
            {
                foreach (Component element in elements)
                {
                    this.Insert(element);
                }
            }
        }

        public void Delete(Component element)
        {
            Component currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                dbTools.Delete(currentElement);
            }
        }

        public void DeleteAll()
        {
            dbTools.DeleteAll<Component>();
        }
    }
}

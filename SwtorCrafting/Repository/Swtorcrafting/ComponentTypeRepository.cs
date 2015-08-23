using SwtorCrafting.Model.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools.Swtorcrafting
{
    public class ComponentTypeRepository : IRepository<ComponentType>
    {
        // Database Tools
        public DataBaseTools dbTools;

        public ComponentTypeRepository()
        {
            dbTools = DataBaseTools.getInstance();
            dbTools.createTableIfNotExist<ComponentType>();
        }
        public ComponentType FindOneById(int id)
        {
            return dbTools.
                FindOneById<ComponentType>(id);
        }

        public List<ComponentType> FindAll()
        {
            return dbTools.FindAll<ComponentType>();
        }

        public void Update(ComponentType element)
        {
            ComponentType currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                currentElement.name = element.name;
                dbTools.Update(currentElement);
            }
        }

        public void Insert(ComponentType element)
        {
            if (element != null)
            {
                dbTools.Insert(element);
            }
        }
        public void InsertAll(List<ComponentType> elements)
        {
            if (elements != null)
            {
                foreach (ComponentType element in elements)
                {
                    this.Insert(element);
                }
            }
        }

        public void Delete(ComponentType element)
        {
            ComponentType currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                dbTools.Delete(currentElement);
            }
        }

        public void DeleteAll()
        {
            dbTools.DeleteAll<ComponentType>();
        }
    }
}

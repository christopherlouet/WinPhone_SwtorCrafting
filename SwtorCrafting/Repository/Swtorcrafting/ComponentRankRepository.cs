using SwtorCrafting.Model.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools.Swtorcrafting
{
    public class ComponentRankRepository : IRepository<ComponentRank>
    {
        // Database Tools
        public DataBaseTools dbTools;

        public ComponentRankRepository()
        {
            dbTools = DataBaseTools.getInstance();
            dbTools.createTableIfNotExist<ComponentRank>();
        }
        public ComponentRank FindOneById(int id)
        {
            return dbTools.
                FindOneById<ComponentRank>(id);
        }
        public List<ComponentRank> FindAll()
        {
            return dbTools.FindAll<ComponentRank>();
        }
        public void Update(ComponentRank element)
        {
            ComponentRank currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                currentElement.rank = element.rank;
                dbTools.Update(currentElement);
            }
        }
        public void Insert(ComponentRank element)
        {
            dbTools.Insert(element);
        }
        public void InsertAll(List<ComponentRank> elements)
        {
            if (elements != null)
            {
                foreach (ComponentRank element in elements)
                {
                    this.Insert(element);
                }
            }
        }
        public void Delete(ComponentRank element)
        {
            ComponentRank currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                dbTools.Delete(currentElement);
            }
        }
        public void DeleteAll()
        {
            dbTools.DeleteAll<ComponentRank>();
        }
    }
}

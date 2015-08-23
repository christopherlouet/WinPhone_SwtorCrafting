using SwtorCrafting.Model.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools.Swtorcrafting
{
    public class RarityRepository : IRepository<Rarity>
    {
        // Database Tools
        public DataBaseTools dbTools;

        public RarityRepository()
        {
            dbTools = DataBaseTools.getInstance();
            dbTools.createTableIfNotExist<Rarity>();
        }
        public Rarity FindOneById(int id)
        {
            Rarity rarity = dbTools.
                FindOneById<Rarity>(id);
            return rarity;
        }

        public List<Rarity> FindAll()
        {
            return dbTools.FindAll<Rarity>();
        }

        public void Update(Rarity element)
        {
            Rarity currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                currentElement.name = element.name;
                currentElement.color = element.color;
                currentElement.level = element.level;
                dbTools.Update(currentElement);
            }
        }

        public void Insert(Rarity element)
        {
            dbTools.Insert(element);
        }

        public void InsertAll(List<Rarity> elements)
        {
            if (elements != null)
            {
                foreach (Rarity element in elements)
                {
                    this.Insert(element);
                }
            }
        }

        public void Delete(Rarity element)
        {
            Rarity currentElement = this.FindOneById(element.id);
            if (currentElement != null)
            {
                dbTools.Delete(currentElement);
            }
        }

        public void DeleteAll()
        {
            dbTools.DeleteAll<Rarity>();
        }
    }
}

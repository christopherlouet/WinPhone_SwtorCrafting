using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwtorCrafting.Model.Swtorcrafting;

namespace SwtorCrafting.Tools.Swtorcrafting
{
    //This class for perform all database CRUD operations   
    public class ItemAttributeRepository : IRepository<ItemAttribute>
    {
        // Database Tools
        public DataBaseTools dbTools;

        public ItemAttributeRepository()
        {
            dbTools = DataBaseTools.getInstance();
            dbTools.createTableIfNotExist<ItemAttribute>();
        }

        // Find one attribute.   
        public ItemAttribute FindOneById(int id)
        {
            return dbTools.
                FindOneById<ItemAttribute>(id);
        }
        // Find all attributes.
        public List<ItemAttribute> FindAll()
        {
            return dbTools.FindAll<ItemAttribute>();
        }

        // Update an attribute.
        public void Update(ItemAttribute itemAttribute)
        {
            ItemAttribute currentAttribute = this.FindOneById(itemAttribute.id);

            if (currentAttribute != null)
            {
                currentAttribute.name = itemAttribute.name;
                dbTools.Update(currentAttribute);
            }
        }
        // Insert an attribute.   
        public void Insert(ItemAttribute itemAttribute)
        {
            if (itemAttribute != null)
            {
                dbTools.Insert(itemAttribute);
            }
        }
        public void InsertAll(List<ItemAttribute> elements)
        {
            if (elements != null)
            {
                foreach (ItemAttribute element in elements)
                {
                    this.Insert(element);
                }
            }
        }
        // Delete an attribute
        public void Delete(ItemAttribute itemAttribute)
        {
            ItemAttribute currentAttribute = this.FindOneById(itemAttribute.id);

            if (currentAttribute != null)
            {
                dbTools.Delete(currentAttribute);
            }
        }
        // Delete all attributes
        public void DeleteAll()
        {
            dbTools.DeleteAll<ItemAttribute>();
        }
    }
}

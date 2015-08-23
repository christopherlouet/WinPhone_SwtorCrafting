using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwtorCrafting.Tools;
using SwtorCrafting.Model.Swtorcrafting;
using SwtorCrafting.Model.View;

namespace SwtorCrafting.API
{
    public class SwtoreliteAPI
    {
        public static string URI_BASE = "https://www.swtorelite.com";

        public static string API_KEY = "lHuAWcKKUoLHnJyeRFlWzhat9aSwwFEJDCH8N49lr4IuwmT5asCXOJIlHs6CVsDPO8SY5oaL6qZHZ51OOHFgkSDFnzOBFvlHjqwR";

        public const string URI_COMPONENTS = "/api/components";

        public const string URI_COMPONENT_RANKS = "/api/componentRanks";

        public const string URI_COMPONENT_TYPES = "/api/componentTypes";

        public const string URI_CRAFT_PLAN_DETAILS = "/api/craftPlans";

        public const string URI_ITEM_ATTRIBUTES = "/api/itemAttributes";

        public const string URI_RARITIES = "/api/rarities";

        protected WSTools wsTools;

        public SwtoreliteAPI()
        {
            wsTools = new WSTools(URI_BASE, API_KEY, WSTools.CHARSET_UTF8);
        }
        public List<Component> LoadComponents()
        {
            List<Component> components = wsTools.GetResource<Component>(URI_COMPONENTS, WSTools.FORMAT_JSON);
            return components;
        }
        public List<ComponentRank> LoadComponentRanks()
        {
            List<ComponentRank> componentRanks = wsTools.GetResource<ComponentRank>(URI_COMPONENT_RANKS, WSTools.FORMAT_JSON);
            return componentRanks;
        }
        public List<ComponentType> LoadComponentTypes()
        {
            List<ComponentType> componentTypes = wsTools.GetResource<ComponentType>(URI_COMPONENT_TYPES, WSTools.FORMAT_JSON);
            return componentTypes;
        }
        public List<CraftPlanDetailView> LoadCraftPlanDetails(string craftSkill)
        {
            string uri = URI_CRAFT_PLAN_DETAILS + "/" + craftSkill;
            List<CraftPlanDetailView> craftPlanDetails = wsTools.GetResource<CraftPlanDetailView>(uri, WSTools.FORMAT_JSON);
            return craftPlanDetails;
        }
        public List<ItemAttribute> LoadItemAttributes()
        {
            List<ItemAttribute> itemAttributes = wsTools.GetResource<ItemAttribute>(URI_ITEM_ATTRIBUTES, WSTools.FORMAT_JSON);
            return itemAttributes;
        }
        public List<Rarity> LoadRarities()
        {
            List<Rarity> rarities = wsTools.GetResource<Rarity>(URI_RARITIES, WSTools.FORMAT_JSON);
            return rarities;
        }
    }
}

using SwtorCrafting.API;
using SwtorCrafting.Model.Swtorcrafting;
using SwtorCrafting.Model.View;
using SwtorCrafting.Tools.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Service
{
    public class SwtorcraftingService
    {
        protected SwtoreliteAPI swtoreliteAPI;
        protected ComponentRankRepository componentRankRepo;
        protected ComponentTypeRepository componentTypeRepo;
        protected RarityRepository rarityRepo;
        protected ItemAttributeRepository itemAttributeRepo;
        protected ComponentRepository componentRepo;
        public List<CraftPlanDetailView> CraftPlanDetails { get; set; }
        public SwtorcraftingService()
        {
            swtoreliteAPI = new SwtoreliteAPI();
            componentRankRepo = new ComponentRankRepository();
            componentTypeRepo = new ComponentTypeRepository();
            rarityRepo = new RarityRepository();
            itemAttributeRepo = new ItemAttributeRepository();
            componentRepo = new ComponentRepository();
        }
        public async Task LoadCraftPlanDetails(string skill)
        {
            CraftPlanDetails = await Task.FromResult<List<CraftPlanDetailView>>(
                swtoreliteAPI.LoadCraftPlanDetails(skill));
        }
        public async Task loadDatabaseWithAPI()
        {
            await Task.Run(() => LoadData());
        }

        protected void LoadData()
        {
            LoadComponentRanks();
            LoadComponentTypes();
            LoadRarities();
            LoadComponents();
            LoadItemAttributes();
        }

        // load component ranks with API and populate database
        protected List<ComponentRank> LoadComponentRanks()
        {
            List<ComponentRank> componentRanks = swtoreliteAPI.LoadComponentRanks();
            componentRankRepo.InsertAll(componentRanks);
            return componentRanks;
        }

        // load component types with API and populate database
        protected List<ComponentType> LoadComponentTypes() {
            
            List<ComponentType> componentTypes = swtoreliteAPI.LoadComponentTypes();
            componentTypeRepo.InsertAll(componentTypes);
            return componentTypes;
        }

        // load rarities with API and populate database
        protected List<Rarity> LoadRarities() {
            List<Rarity> rarities = swtoreliteAPI.LoadRarities();
            rarityRepo.InsertAll(rarities);
            return rarities;
        }

        // load components with API and populate database
        protected List<Component> LoadComponents()
        {
            List<Component> components = swtoreliteAPI.LoadComponents();
            componentRepo.InsertAll(components);
            return components;
        }

        // load attributes with API and populate database
        protected List<ItemAttribute> LoadItemAttributes()
        {
            List<ItemAttribute> itemAttributes = swtoreliteAPI.LoadItemAttributes();
            itemAttributeRepo.InsertAll(itemAttributes);
            return itemAttributes;
        }

            
    }
}

using SwtorCrafting.Model.Swtorcrafting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Model.View
{
    public class CraftPlanDetailView
    {
        public int Id { get; set; }

        public string CraftSkillName { get; set; }

        public string ItemName { get; set; }

        public int ItemLevel { get; set; }

        public string ItemRarityName { get; set; }

        public string ItemTypeName { get; set; }
    }
}

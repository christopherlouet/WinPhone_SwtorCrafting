using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Model.Swtorcrafting
{
    public class ComponentRank
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        [SQLite.NotNull]
        public int rank { get; set; }
    }
}

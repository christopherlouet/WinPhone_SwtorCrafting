using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Model.Swtorcrafting
{
    public class Rarity
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        [SQLite.NotNull, SQLite.MaxLength(45)]
        public string name { get; set; }
        [SQLite.NotNull, SQLite.MaxLength(45)]
        public string color { get; set; }
        [SQLite.NotNull]
        public int level { get; set; }
    }
}

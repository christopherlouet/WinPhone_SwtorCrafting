using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Model.Swtorcrafting
{
    public class ItemAttribute
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        [SQLite.NotNull, SQLite.MaxLength(45)]
        public string name { get; set; }
    }
}

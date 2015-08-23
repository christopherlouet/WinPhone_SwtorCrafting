using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using SwtorCrafting.Model;
using System.IO;
using Windows.Storage;

namespace SwtorCrafting.Tools
{
    public class DataBaseTools : SQLiteConnection 
    {
        // database
	    public const int DB_VERSION = 1;
	    public const string DB_NAME = "swtorelite.sqlite";
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME));

        private static DataBaseTools mInstance = null;

        private DataBaseTools(string dbName) : base(dbName) { }
        public static DataBaseTools getInstance()
        {
            if (mInstance == null)
            {
                mInstance = new DataBaseTools(DB_PATH);
            }
            return mInstance;
        }
        public T FindOneById<T>(int id) where T : new()
        {
            String tableName = typeof(T).FullName;
            string query = "SELECT * FROM " + tableName + " WHERE id = ?";
            SQLiteCommand cmd = this.CreateCommand(query, id);
            var result = cmd.ExecuteQuery<T>();
            List<T> elements = result.ToList<T>();
            return elements.FirstOrDefault();
        }
        public List<T> FindAll<T>() where T : new()
        {
            List<T> elements = this.Table<T>().ToList<T>();
            return elements;
        }
        public void Update<T>(T entity)
        {
            this.RunInTransaction(() =>
            {
                base.Update(entity);
            });
        }
        public void Insert<T>(T entity)
        {
            this.RunInTransaction(() =>
            {
                base.Insert(entity);
            });
        }
        public void Delete<T>(T entity)
        {
            this.RunInTransaction(() =>
            {
                base.Delete(entity);
            });
        }
        public void DeleteAll<T>()
        {
            this.RunInTransaction(() =>   
            {
                base.DropTable<T>();
                base.CreateTable<T>();
                base.Dispose();
                base.Close();
            });   
        }
        public bool createTableIfNotExist<T>()
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    base.CreateTable<T>();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }  
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

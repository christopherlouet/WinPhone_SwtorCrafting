using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools
{
    public interface IRepository<T>
    {
        T FindOneById(int id);
        List<T> FindAll();
        void Update(T element);
        void Insert(T element);
        void InsertAll(List<T> elements);
        void Delete(T element);
        void DeleteAll();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseConnector.Interfaces
{
    public interface ICRUD<T>
    {
        public T GetById(int Id);
        public IEnumerable<T> GetAll();
        public int Insert(T item);
        public int Update(T item);
        public int delete(int Id);
    }
}

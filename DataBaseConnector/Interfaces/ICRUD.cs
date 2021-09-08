using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseConnector.Interfaces
{
    public interface ICRUD<T>
    {
        public T GetById(int Id);
        public IEnumerable<T> GetAll();
        public void Insert(T item);
        public void Update(T item);
        public void delete(int Id);
    }
}

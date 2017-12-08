using System;

namespace Herotale.IRepositories
{
    public interface IRepository<T> where T:class 
    {
        bool Update(T obj);
        bool Insert(T obj);
        bool Remove(T obj);
    }
}
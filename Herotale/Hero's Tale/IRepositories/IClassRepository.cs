using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IClassRepository : IRepository<Class>
    {
        Class Get(int id);
    }
}
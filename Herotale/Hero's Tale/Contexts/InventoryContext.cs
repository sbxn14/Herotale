using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.Contexts
{
    public class InventoryContext
    {
        readonly IInventoryRepository Rep;

        public InventoryContext(IInventoryRepository rep)
        {
            Rep = rep;
        }

        public Inventory GetById(int id)
        {
            return Rep.Get(id);
        }

        public bool Insert(Inventory inven)
        {
            return Rep.Insert(inven);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.Contexts
{
    public class ClassContext
    {
        readonly IClassRepository Rep;

        public ClassContext(IClassRepository rep)
        {
            Rep = rep;
        }

        public Class GetById(int id)
        {
            return Rep.Get(id);
        }
    }
}
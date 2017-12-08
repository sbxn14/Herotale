using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.Contexts
{
    public class RaceContext
    {
        readonly IRaceRepository Rep;

        public RaceContext(IRaceRepository rep)
        {
            Rep = rep;
        }

        public Race GetById(int id)
        {
            return Rep.Get(id);
        }
    }
}
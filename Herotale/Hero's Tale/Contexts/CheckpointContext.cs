using Herotale.IRepositories;
using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herotale.Contexts
{
	public class CheckpointContext
	{
		readonly ICheckpointRepository Rep;

		public CheckpointContext(ICheckpointRepository rep)
		{
			Rep = rep;
		}
		public Checkpoint GetById(int id)
		{
			return Rep.GetById(id);
		}
		public List<Checkpoint> GetAll()
		{
			return Rep.GetAll();
		}
	}
}
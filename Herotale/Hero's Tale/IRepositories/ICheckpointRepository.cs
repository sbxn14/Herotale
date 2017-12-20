using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herotale.IRepositories
{
	public interface ICheckpointRepository
	{
		Checkpoint GetById(int id);
		List<Checkpoint> GetAll();
		Checkpoint GetByEvent(int Event);
	}
}
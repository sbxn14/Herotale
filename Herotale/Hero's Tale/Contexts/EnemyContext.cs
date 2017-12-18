using Herotale.IRepositories;
using Herotale.Models;
using System.Collections.Generic;

namespace Herotale.Contexts
{
	public class EnemyContext
    {
		readonly IEnemyRepository Rep;

		public EnemyContext(IEnemyRepository rep)
		{
			Rep = rep;
		}
		public int Randomizer()
		{
			return Rep.Randomizer();
		}
		public Enemy Get(int id)
		{
			return Rep.Get(id);
		}
		public List<Enemy> GetAll()
		{
			return Rep.GetAll();
		}
		public Enemy RandomMob(int RandomNumber, List<Enemy> Enemies)
		{
			return Rep.RandomMob(RandomNumber, Enemies);
		}
	}
}
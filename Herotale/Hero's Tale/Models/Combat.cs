using System;

namespace Herotale.Models
{
	public class Combat
	{
		public Character Chr { get; set; }
		public Enemy Enem { get; set; }

		//		public Story StartCombat(Character Ch, Enemy En)
		//		{
		//			Chr = Ch;
		//			Enem = En;
		//			Random r = new Random();
		//			int start = r.Next(1, 2); // 1 = player start, 2 = enemy start
		//			Story Str = new Story
		//			{
		//				Char = Chr,
		//				Enem = Enem,
		//				Sgt = new Segment()
		//			};
		//			Str.Sgt.Title = "Battle to the DEATH! Featuring The Mighty " + Chr.Name + " And the Puny " + Enem.Name;

		//			if (start == 1)
		//			{
		//				Str.Sgt.Text = "A Wild " + Enem.Name + " Has Appeared!" + Environment.NewLine + "It's your turn!";
		//			}
		//			else if (start == 2)
		//			{

		//				Str.Sgt.Text = "A Wild " + Enem.Name + " Has Appeared!" + Environment.NewLine + "It's" + Enem.Name + "'s turn!";
		//			}

		//			//Str.CombatTurn = start;
		//			return Str;
		//		}

		//		public Story EnemyTurn(Story Str)
		//		{
		//			Random r = new Random();
		//			int accuracy = r.Next(0, Chr.Speed); //dodging based on Speed

		//			if (accuracy >= 15) //hit
		//			{
		//				int defpercentage = Chr.Defense / 100; //amount of damage based on Defense
		//				int Health = Str.Char.Health - (Enem.AttackPower * defpercentage);
		//				// add story segment
		//				Str.Char = new Character(Chr, Health);
		//			}
		//			else if (accuracy < 15)//miss
		//			{
		//				//add story segment
		//				Str.Char = Chr;
		//			}

		//			if (Str.Char.Health <= 0)
		//			{
		//				Str = PlayerDeath(Str);
		//			}
		//			return Str;
		//		}

		//		public Story PlayerTurn(Story Str)
		//		{
		//			Random r = new Random();
		//			int accuracy = r.Next(1, 100); //0 = miss, 1 = hit.

		//			if (accuracy > 0)
		//			{
		//				Str.Sgt.Text = "You caused the " + Enem.Name + " " + Chr.AttackPower + " Damage!" + Environment.NewLine + "Type 'Continue' and press Enter to start the Enemy's Turn.";
		//				Str.Enem.Health = Enem.Health - Chr.AttackPower;
		//			}
		//			else if (accuracy == 0)
		//			{
		//				Str.Enem = Enem;
		//				Str.Sgt.Text = "You Missed!" + Environment.NewLine + "Type 'Continue' and press Enter to start the Enemy's Turn.";
		//			}

		//			if (Enem.Health <= 0)
		//			{
		//				Str = EnemyDeath(Str);
		//			}
		//			return Str;
		//		}

		//		public Story Heal(Story Str)
		//		{
		//			int Health = Chr.Health + 100;

		//			if (Health > Chr.MaxHealth)
		//			{
		//				Health = Chr.MaxHealth;
		//			}

		//			if (Chr.Cl.Focus == 1)
		//			{
		//				Str.Char = new Character(Chr, Health);
		//				Str.Sgt.Text = "You dust yourself off and heal yourself." + Environment.NewLine + "Your Health is now " + Chr.Health + "/" + Chr.MaxHealth + Environment.NewLine + "Type 'Continue' and press Enter";
		//			}
		//			else if (Chr.Cl.Focus == 2)
		//			{
		//				Str.Char = new Character(Chr, Health);
		//				Str.Sgt.Text = "You heal yourself in divine light." + Environment.NewLine + "Your Health is now " + Chr.Health + "/" + Chr.MaxHealth + Environment.NewLine + "Type 'Continue' and press Enter";

		//			}
		//			else if (Chr.Cl.Focus == 3)
		//			{
		//				Str.Char = new Character(Chr, Health);
		//				Str.Sgt.Text = "You drink a potion." + Environment.NewLine + "Your Health is now " + Chr.Health + "/" + Chr.MaxHealth + Environment.NewLine + "Type 'Continue' and press Enter";

		//			}
		//			return Str;
		//		}

		//		public Story PlayerDeath(Story Str)
		//		{
		//			//Str.CombatTurn = 3;

		//			Str.Sgt.Title = "The End";
		//			Str.Sgt.Text = "You died bro. RIP. Press reset to start over.";
		//			return Str;
		//		}

		//		public Story EnemyDeath(Story Str)
		//		{
		//			//Str.CombatTurn = 4;

		//			Str.Sgt.Title = "Prey Slaughtered";
		//			Str.Sgt.Text = "You have slain the " + Enem.Name + "!" + Environment.NewLine + "Congratulations! Type 'Continue' and press Enter.";
		//			return Str;
		//		}
	}
}
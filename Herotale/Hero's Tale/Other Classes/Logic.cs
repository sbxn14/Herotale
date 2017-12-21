﻿using Herotale.Contexts;
using Herotale.Models;
using Herotale.Models.Enums;
using Herotale.MSSQL_Repositories;
using Herotale.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Herotale
{
	public class Logic
	{
		private CharacterContext CharCon = new CharacterContext(new MssqlCharacterRep());
		private EnemyContext EnemCon = new EnemyContext(new MssqlEnemyRep());
		private CheckpointContext Cpcon = new CheckpointContext(new MSSQLCheckpointREP());
		private ItemContext ItemCon = new ItemContext(new MssqlItemRep());
		private Item it = null;
		private int OldProgress;
		private int progress;
		//bool InCombat;
		//int CombatProgress;

		public StoryViewModel Hub(Input Inp)
		{
			progress = Inp.Char.Cp.Event;
			//Combat cb = new Combat();

			if (progress == 4 && progress > 4)
			{
				OldProgress = 5;
			}

			Story Str = GetAllStories(Inp.Char)[(progress - 1)]; //gets story based on progress
																 //Str.Enem = CheckForEnemies(Str);

			int CmNr = CommandHandler(Inp); //converts input message to a number.

			if (progress == 0) //if progress is somehow 0, it gets reset to the first screen of the game.
			{
				progress = 3;
				OldProgress = 3;
			}
			else if (CmNr == 5) //inventory
			{
				OldProgress = progress;
				progress = 1;
			}
			else if (CmNr == 8) //statistics
			{
				OldProgress = progress;
				progress = 2;
			}
			else if (CmNr == 18) //helpscreen
			{
				OldProgress = progress;
				progress = 4;
			}
			else if (CmNr == Str.Sgt.Choice1 || CmNr == Str.Sgt.Choice2) //if cmnr equals a choice then do nothing (aka go on)
			{

			}
			else if (CmNr != Str.Sgt.Choice1 && progress != 0 || CmNr != Str.Sgt.Choice2 && progress != 0) // if cmnr doesn't equal a choice and progress is not 0. then cmnr = 0.
			{
				CmNr = 0;
			}

			if (progress == 4 || progress == 1 || progress == 2) //if progress is help/inventory/statistics
			{
				if (CmNr == 1 || CmNr == 10) // if command is Close or Continue then continue to last screen.
				{
					progress = progress;
				}
			}
			else if (progress == 3) //welcome
			{
				if (CmNr == 1) //continue to tutorial
				{
					OldProgress = 5;
					progress = 10;
				}
				else if (CmNr == 2) // skip tutorial -> to act one.
				{
					OldProgress = 5;
					progress = 5;
				}
			}
			else if (progress == 10)//first tutorial (not the one you can call again and again)
			{
				if (CmNr == 1)
				{
					OldProgress = 5;
					progress = 5;
				}
			}
			else if (progress == 5) //act one - plains
			{
				if (CmNr == 11)//forward
				{
					OldProgress = 6;
					progress = 6;
				}
				else if (CmNr == 17)//run
				{
					//ending 1 (running at start/not entering castle)
				}
			}
			else if (progress == 6)//act one - castle courtyard
			{
				if (CmNr == 9) //Open
				{
					OldProgress = 7;
					progress = 7;
				}
			}
			else if (progress == 7)//act one - castle courtyard - door
			{
				if (CmNr == 11) //forward
				{
					OldProgress = 8;
					progress = 8;
				}
			}
			else if (progress == 8)//act one - atrium
			{
				if (CmNr == 11) // forward
				{
					OldProgress = 9;
					progress = 9;
				}
			}
			else if (progress == 9)//act one - Hallway
			{
				//if (CmNr == 3)
				//{
				//	Str = cb.StartCombat(Inp.Char, Str.Enem);
				//	InCombat = true;
				//	CombatProgress = Str.CombatTurn;
				//	Str.Char = Inp.Char;
				//}
				//if (CmNr == 1 && CombatProgress == 1)
				//{
				//	CombatProgress = 2;
				//}
				//else if (CmNr == 1 && CombatProgress == 2)
				//{
				//	CombatProgress = 1;
				//}
				if (CmNr == 3) //look at
				{
					OldProgress = 11;
					progress = 11;
				}
				else if (CmNr == 11) //forward
				{
					OldProgress = 13;
					progress = 13;
				}
			}
			else if (progress == 11) //item after look at
			{
				if (CmNr == 10) //continue
				{
					OldProgress = 13;
					progress = 13;
				}
				else if (CmNr == 16) //pickup
				{
					Item it = ItemCon.GetRandomItem();
					//add to inventory
				}
			}

			//if (CombatProgress == 1) //player turn
			//{
			//	Str = cb.PlayerTurn(Str);
			//	CombatProgress = Str.CombatTurn;
			//}
			//else if (CombatProgress == 2) //enemy turn
			//{
			//	Str = cb.EnemyTurn(Str);
			//	CombatProgress = Str.CombatTurn;
			//}

			//continue with story here. if desired.

			StoryViewModel Mod = new StoryViewModel
			{
				Inp = Inp,
				Str = Str
			};

			//if (InCombat)
			//{
			//	if (CmNr == 4)
			//	{
			//		Str.Char = Inp.Char;
			//		Str = cb.Heal(Str);
			//	}
			//	//skip the getting-of-story-segments cause of combat
			//}
			//else if (CombatProgress == 4) //enemydeath & Resets health to full.
			//{
			//	progress += 1;
			//	InCombat = false;
			//	Str.Char = new Character(Str.Char, Str.Char.MaxHealth);
			//}
			//else if (CombatProgress == 3)//playerdeath
			//{
			//	//insert player died ending
			//}
			//else
			//{
			Str.Sgt = GetAllStories(Inp.Char)[(progress - 1)].Sgt; //get story segment based on progress (not in combat)
																   //}

			if (Str.Sgt.Text.Contains("{"))
			{
				Str = Edit(Str); //replace any {statements} with the fitting replacement.
			}

			if (Str.Sgt.Id != 1 && Str.Sgt.Id != 2 && Str.Sgt.Id != 4) //if story segment is not help/inventory/statistics. Set checkpoint on current progress.
			{
				Checkpoint k = Cpcon.GetByEvent(progress);
				Inp.Char = new Character(k, Inp.Char);
			}

			CharCon.Update(Inp.Char);//update character
			Mod.Inp = Inp;
			Mod.Str.Char = Inp.Char;
			return Mod;
		}

		public Story Edit(Story str)
		{
			string i = str.Sgt.Text;

			i = i.Replace("{name}", str.Char.Name);
			i = i.Replace("{gender}", str.Char.Gender);
			i = i.Replace("{race}", str.Char.Rc.Name);
			i = i.Replace("{class}", str.Char.Cl.Name);
			i = i.Replace("{HP}", str.Char.Health.ToString());
			i = i.Replace("{AP}", str.Char.AttackPower.ToString());
			i = i.Replace("{DEF}", str.Char.Defense.ToString());
			i = i.Replace("{SPD}", str.Char.Speed.ToString());
			i = i.Replace("{char}", str.Char.Name);

			if(it != null)
			{
				i = i.Replace("{item}", it.Name);
			}

			//i = i.Replace("{Skeleton Warrior}", EnemCon.GetByName("Skeleton Warrior").Name);
			//if (progress == 9)
			//{
			//	i = i.Replace("{Monster}", EnemCon.GetByName("Skeleton Warrior").Name);
			//}
			//add more as combat situations increase.
			//str.Enem = EnemCon.GetByName(i);
			str.Sgt.Text = i;
			return str;
		}

		public int CommandHandler(Input In)
		{
			int result = 0;

			if (!String.IsNullOrEmpty(In.Message))
			{
				if (In.Message.Contains("pick up") || In.Message.Contains("look at"))
				{
					int index = In.Message.IndexOf(' ');
					index = In.Message.IndexOf(' ', index + 1);
					In.Message = In.Message.Substring(0, index);
					In.Message = In.Message.Replace(" ", string.Empty);

					In.Message = char.ToUpper(In.Message[0]) + In.Message.Substring(1);
				}
				else if (In.Message.Contains("statistics") || In.Message.Contains("Statistics"))
				{
					In.Message = "Stats";
				}
				else
				{
					In.Message = In.Message.Split(' ').FirstOrDefault();
					In.Message = char.ToUpper(In.Message[0]) + In.Message.Substring(1);
				}
			}

			if (Enum.GetName(typeof(Commands), Commands.Continue) == In.Message)
			{
				result = (int)Commands.Continue;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Skip) == In.Message)
			{
				result = (int)Commands.Skip;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Attack) == In.Message)
			{
				result = (int)Commands.Attack;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Heal) == In.Message)
			{
				result = (int)Commands.Heal;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Inventory) == In.Message)
			{
				result = (int)Commands.Inventory;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Equip) == In.Message)
			{
				result = (int)Commands.Equip;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Unequip) == In.Message)
			{
				result = (int)Commands.Unequip;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Stats) == In.Message)
			{
				result = (int)Commands.Stats;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Open) == In.Message)
			{
				result = (int)Commands.Open;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Close) == In.Message)
			{
				result = (int)Commands.Close;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Forward) == In.Message)
			{
				result = (int)Commands.Forward;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Back) == In.Message)
			{
				result = (int)Commands.Back;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Left) == In.Message)
			{
				result = (int)Commands.Left;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Right) == In.Message)
			{
				result = (int)Commands.Right;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Lookat) == In.Message)
			{
				result = (int)Commands.Lookat;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Pickup) == In.Message)
			{
				result = (int)Commands.Pickup;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Run) == In.Message)
			{
				result = (int)Commands.Run;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Help) == In.Message)
			{
				result = (int)Commands.Help;
			}
			else if (Enum.GetName(typeof(Commands), Commands.Use) == In.Message)
			{
				result = (int)Commands.Use;
			}

			return result;
		}

		public List<Story> GetAllStories(Character Chara)
		{
			SqlDataReader reader = null;

			List<Story> result = new List<Story>();
			string query = "Select * from dbo.Stories";

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
			{
				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					try
					{
						cmd.Connection.Open();
						cmd.Prepare();
						reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							Story obj = new Story();
							obj.Char = Chara;
							obj.Sgt.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							obj.Sgt.Title = reader.GetString(reader.GetOrdinal("Title"));
							obj.Sgt.Text = reader.GetString(reader.GetOrdinal("Text"));
							obj.Sgt.Choice1 = reader.GetInt32(reader.GetOrdinal("Choice 1"));
							obj.Sgt.Choice2 = reader.GetInt32(reader.GetOrdinal("Choice 2"));
							result.Add(obj);
						}
					}
					catch (SqlException e)
					{
						throw e;
					}
					finally
					{
						cmd.Connection.Close();
						reader?.Close();
					}
					return result;
				}
			}
		}
		public Enemy CheckForEnemies(Story Str)
		{
			Enemy En = new Enemy();
			if (Str.Sgt.Text.Contains("{Monster}"))
			{
				if (progress == 9)
				{
					En = EnemCon.GetByName("Skeleton Warrior");
				}
				//add more as combat situations increase.
			}

			return En;
		}
	}
}

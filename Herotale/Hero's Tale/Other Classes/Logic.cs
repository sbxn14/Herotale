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
		private InventoryContext InvCon = new InventoryContext(new MssqlInventoryRep());
		Checkpoint k = null;
		private int OldProgress;
		private int progress;
		private int WhatItem;
		//bool InCombat;
		//int CombatProgress;

		public StoryViewModel Hub(Input Inp)
		{
			if (Inp.Title == "Inventory")
			{
				progress = 1;
				OldProgress = Inp.Char.Cp.Event;
			}
			else if (Inp.Title == "Helpscreen" && Inp.Char.Cp.Event != 10)
			{
				progress = 4;
				OldProgress = Inp.Char.Cp.Event;
			}
			else if (Inp.Title == "Statistics")
			{
				progress = 2;
				OldProgress = Inp.Char.Cp.Event;
			}
			else
			{
				progress = Inp.Char.Cp.Event;
			}

			//Combat cb = new Combat();

			Story Str = GetAllStories(Inp.Char)[(progress - 1)]; //gets story based on progress
			Str.Char = Inp.Char;
			int CmNr = CommandHandler(Inp); //converts input message to a number.

			if (progress == 0) //if progress is somehow 0, it gets reset to the first screen of the game.
			{
				progress = 3;
				OldProgress = 3;
			}
			else if (CmNr == 5) //inventory
			{
				OldProgress = Inp.Char.Cp.Event;
				progress = 1;
			}
			else if (CmNr == 8) //statistics
			{
				OldProgress = Inp.Char.Cp.Event;
				progress = 2;
			}
			else if (CmNr == 18) //helpscreen
			{
				OldProgress = Inp.Char.Cp.Event;
				progress = 4;
			}
			else if (CmNr == Str.Sgt.Choice1 || CmNr == Str.Sgt.Choice2) //if cmnr equals a choice then do nothing (aka go on)
			{

			}
			else if (progress != 1 && progress != 2 && progress != 4)// if cmnr doesn't equal a choice and progress is not 0. then cmnr = 0.
			{
				if (CmNr != Str.Sgt.Choice1 && progress != 0 || CmNr != Str.Sgt.Choice2 && progress != 0)
				{
					CmNr = 0;
				}
			}

			if (progress == 1)//inventory
			{
				if (Inp.Message.Contains("equip") || Inp.Message.Contains("Equip") || Inp.Message.Contains("unequip") || Inp.Message.Contains("unequip"))
				{
					string WhatItem = Inp.Message.Split(' ').FirstOrDefault();
					WhatItem = char.ToUpper(Inp.Message[0]) + Inp.Message.Substring(1);
				}
			}

			if (progress == 1)//inventory
			{
				if (CmNr == 6)//equip
				{
					if (WhatItem != 0)
					{
						Inp.Char = CharCon.EquipItem(WhatItem, Inp.Char);
						progress = OldProgress;
					}
				}
				else if (CmNr == 7)//unequip
				{
					if (WhatItem != 0)
					{
						Inp.Char = CharCon.UnequipItem(WhatItem, Inp.Char);
						progress = OldProgress;
					}
				}
			}

			if (progress == 4 || progress == 1 || progress == 2) //if progress is help/inventory/statistics
			{
				if (CmNr == 1 || CmNr == 10) // if command is Close or Continue then continue to last screen.
				{
					progress = OldProgress;
				}
			}
			else if (progress == 3) //welcome
			{
				if (CmNr == 1) //continue to tutorial
				{
					OldProgress = 5;
					progress = 10;
				}
				else if (CmNr == 2) // skip tutorial -> to story.
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
			else if (progress == 5) //forest
			{
				if (CmNr == 11)//forward
				{
					OldProgress = 6;
					progress = 6;
				}
				else if (CmNr == 17)//run
				{
					progress = 23;//bad ending
					OldProgress = 23;
				}
			}
			else if (progress == 6)//castle courtyard
			{
				if (CmNr == 9) //Open
				{
					OldProgress = 7;
					progress = 7;
				}
			}
			else if (progress == 7)//castle courtyard - door
			{
				if (CmNr == 11) //forward
				{
					OldProgress = 8;
					progress = 8;
				}
			}
			else if (progress == 8)//atrium
			{
				if (CmNr == 11) // forward
				{
					OldProgress = 9;
					progress = 9;
				}
			}
			else if (progress == 9)//Hallway before itempickup
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
				if (CmNr == 15) //look at
				{
					OldProgress = 11;
					progress = 11;
					Inp.Char.TemporaryItem = ItemCon.GetRandomItem().Id; //saves the id of the randomitem in character
				}
				else if (CmNr == 11) //forward
				{
					OldProgress = 14;
					progress = 14;
				}
				else if (CmNr == 0 && (Str.It.Name == null || Inp.Char.TemporaryItem == 0))
				{
					Inp.Char.TemporaryItem = ItemCon.GetRandomItem().Id; //saves the id of the randomitem in character
				}
			}
			else if (progress == 11) //item after look at
			{
				if (CmNr == 11) //continue or forward
				{
					OldProgress = 13;
					progress = 13;
				}
				else if (CmNr == 16) //pickup
				{
					Str.Char = Inp.Char;
					Str = InvCon.AddItem(ItemCon.GetById(Inp.Char.TemporaryItem), Str); //additem to inventory
					OldProgress = 12;
					progress = 12;
				}
				else if ((CmNr == 0 || CmNr == 1 || CmNr == 10) && Inp.Char.TemporaryItem == 0)
				{
					Inp.Char.TemporaryItem = ItemCon.GetRandomItem().Id; //saves the id of the randomitem in character
				}
			}
			else if (progress == 12)//Hallway after item pickup
			{
				if (CmNr == 11) //forward
				{
					OldProgress = 13;
					progress = 13;
				}
			}
			else if (progress == 13)//infront of upstairs door
			{
				if (CmNr == 9)//open
				{
					progress = 15;
					OldProgress = progress;
				}
			}
			else if (progress == 15)//infront of OPEN upstairs door
			{
				if (CmNr == 11)//forward
				{
					progress = 14;
					OldProgress = progress;
				}
			}
			else if (progress == 14)//the room
			{
				if (CmNr == 19)//use
				{
					progress = 16;
					OldProgress = progress;
				}
			}
			else if (progress == 16)//lever pulled, sound downstairs.
			{
				if (CmNr == 12)//back
				{
					progress = 17;
					OldProgress = progress;
				}
			}
			else if (progress == 17)//hallway faced towards stairs
			{
				if (CmNr == 11)//forward
				{
					progress = 18;
					OldProgress = progress;
				}
			}
			else if (progress == 18)//top of staircase
			{
				if (CmNr == 11)//forward
				{
					progress = 19;
					OldProgress = progress;
				}
			}
			else if (progress == 19)//atrium infront of open door
			{
				if (CmNr == 11)//forward
				{
					progress = 20;
					OldProgress = progress;
				}
			}
			else if (progress == 20)//Dark room
			{
				if (CmNr == 15)//look at light
				{
					progress = 21;
					OldProgress = progress;
				}
			}
			else if (progress == 21)//Candle and button
			{
				if (CmNr == 19)//use
				{
					progress = 22; //good ending
					OldProgress = progress;
				}
			}
			Str.Char = Inp.Char;

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
			if (progress != 0)
			{
				Str.Sgt = GetAllStories(Inp.Char)[(progress - 1)].Sgt; //get story segment based on progress (not in combat)
			}
			//}

			if (Str.Sgt.Text.Contains("{"))
			{
				Str = Edit(Str); //replace any {statements} with the fitting replacement.
			}

			if (Str.Sgt.Id != 1 && Str.Sgt.Id != 2 && Str.Sgt.Id != 4) //if story segment is not help/inventory/statistics. Set checkpoint on current progress.
			{
				k = Cpcon.GetByEvent(progress);

				Inp.Char = new Character(k, Inp.Char);
			}

			CharCon.Update(Inp.Char);//update character
			Mod.Inp = Inp;
			Mod.Str.Char = Inp.Char;
			Mod.Str.It = Str.It;
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
			i = i.Replace("{slot1}", str.Char.Slot1.Name);
			i = i.Replace("{slot2}", str.Char.Slot2.Name);
			i = i.Replace("{slot3}", str.Char.Slot3.Name);
			i = i.Replace("{inven1}", str.Char.Inven.Items[0].Name);
			i = i.Replace("{inven2}", str.Char.Inven.Items[1].Name);
			i = i.Replace("{inven3}", str.Char.Inven.Items[2].Name);
			i = i.Replace("{inven4}", str.Char.Inven.Items[3].Name);
			i = i.Replace("{inven5}", str.Char.Inven.Items[4].Name);
			i = i.Replace("{inven6}", str.Char.Inven.Items[5].Name);
			i = i.Replace("{inven7}", str.Char.Inven.Items[6].Name);
			i = i.Replace("{inven8}", str.Char.Inven.Items[7].Name);
			i = i.Replace("{inven9}", str.Char.Inven.Items[8].Name);
			i = i.Replace("{inven10}", str.Char.Inven.Items[9].Name);

			if (str.Char.TemporaryItem != 0)
			{
				i = i.Replace("{item}", ItemCon.GetById(str.Char.TemporaryItem).Name);
			}

			str.Sgt.Text = i;
			return str;
		}

		public int CommandHandler(Input In)
		{
			int result = 0;

			if (!String.IsNullOrEmpty(In.Message))
			{
				if (In.Message.Contains("pick up") || In.Message.Contains("look at") || In.Message.Contains("Pick Up") || In.Message.Contains("Pick up") || In.Message.Contains("Look At"))
				{
					try
					{
						int index = In.Message.IndexOf(' ');
						index = In.Message.IndexOf(' ', index + 1);
						In.Message = In.Message.Substring(0, index);
						In.Message = In.Message.Replace(" ", string.Empty);

						In.Message = char.ToUpper(In.Message[0]) + In.Message.Substring(1);
					}
					catch
					{
						return 0;
					}

				}
				else if (In.Message.Contains("statistics") || In.Message.Contains("Statistics"))
				{
					In.Message = "Stats";
				}
				else
				{
					if (In.Message.Contains("Equip") || In.Message.Contains("unequip") || In.Message.Contains("equip") || In.Message.Contains("unequip"))
					{
						WhatItem = Convert.ToInt32(In.Message.Split(' ').Last()); //puts the name or number of the item in a string variable
					}
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
		//public Enemy CheckForEnemies(Story Str)
		//{
		//	Enemy En = new Enemy();
		//	if (Str.Sgt.Text.Contains("{Monster}"))
		//	{
		//		if (progress == 9)
		//		{
		//			En = EnemCon.GetByName("Skeleton Warrior");
		//		}
		//		//add more as combat situations increase.
		//	}

		//	return En;
		//}
	}
}

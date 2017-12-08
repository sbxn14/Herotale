using Herotale.Contexts;
using Herotale.Database;
using Herotale.Models;
using Herotale.Models.Enums;
using Herotale.MSSQL_Repositories;
using System;
using System.Linq;

namespace Herotale
{
	public class Logic
	{
		private CharacterContext CharCon = new CharacterContext(new MssqlCharacterRep());
		private int OldProgress;

		public Story Hub(Input Inp)
		{
			int progress = Inp.Char.Cp.Event;

			int CmNr = CommandHandler(Inp);
			if (progress == 3)
			{
				if (CmNr == (Int32)Commands.Continue) //view tutorial/Helpscreen
				{
					progress = 4;
				}
				else if(CmNr == (Int32)Commands.Skip) // skip tutorial/Helpscreen
				{
					progress = 5;
				}
				else if(CmNr == (Int32)Commands.Inventory) // view Inventory 
				{
					OldProgress = progress;
					progress = 1;
				}
				else if (CmNr == (Int32)Commands.Stats) // view statistics
				{
					OldProgress = progress;
					progress = 2;
				}
				else if(CmNr == (Int32)Commands.Help) // view Helpscreen/tutorial
				{
					OldProgress = progress;
					progress = 4;
				}

			}


			Story Str = new Story
			{
				Sgt = Datamanager.SegmentList[(progress - 1)],
				Char = Inp.Char
			};

			return Str;
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
				else
				{
					In.Message = In.Message.Split(' ').FirstOrDefault();
					In.Message = char.ToUpper(In.Message[0]) + In.Message.Substring(1);
				}
			}

			if (In.Message == "Continue")
			{
				result = 1;
			}
			else if (In.Message == "Skip")
			{
				result = 2;
			}
			else if (In.Message == "Attack")
			{
				result = 3;
			}
			else if (In.Message == "Heal")
			{
				result = 4;
			}
			else if (In.Message == "Inventory")
			{
				result = 5;
			}
			else if (In.Message == "Equip")
			{
				result = 6;
			}
			else if (In.Message == "Unequip")
			{
				result = 7;
			}
			else if (In.Message == "Stats")
			{
				result = 8;
			}
			else if (In.Message == "Open")
			{
				result = 9;
			}
			else if (In.Message == "Close")
			{
				result = 10;
			}
			else if (In.Message == "Forward")
			{
				result = 11;
			}
			else if (In.Message == "Back")
			{
				result = 12;
			}
			else if (In.Message == "Left")
			{
				result = 13;
			}
			else if (In.Message == "Right")
			{
				result = 14;
			}
			else if (In.Message == "Lookat")
			{
				result = 15;
			}
			else if (In.Message == "Pickup")
			{
				result = 16;
			}
			else if (In.Message == "Run")
			{
				result = 17;
			}
			else if (In.Message == "Help")
			{
				result = 18;
			}

			return result;
		}
	}
}
using Herotale.ViewModels;

namespace Herotale.Models
{
	public class Story
	{
		public Segment Sgt = new Segment();
		public Character Char = new Character();   //has checkpoint
	}

	public class Segment 
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public int Choice1 { get; set; }
		public int Choice2 { get; set; }
	}
}

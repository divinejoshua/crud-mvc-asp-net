using System;
namespace RunGroopWebApp.Models
{
	public class AppUser
	{
		public int? Pace { get; set; }
		public int? Millage { get; set; }
		public Address? Address { get; set; }
		public ICollection<Club>? Clubs { get; set; }
		public ICollection<Race>? Races {get; set;}
	}
}


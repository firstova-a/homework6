using System;
using System.Collections.Generic;
using System.Text;

namespace hw6Server.EF
{
	class Atist
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public byte[] PasswordHash { get; set; }
		public string ContactEmail { get; set; }
		public string FIO { get; set; }
		public string ShortInfo { get; set; }
		public string ContactPhone { get; set; }

		public List<Post> Posts { get; set; }
		public List<Art> Arts { get; set; }
		public List<Project> Projects { get; set; }
		public List<Gallery> Galleries { get; set; }
		public List<ArtOrder> Orders { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace hw6Server.EF
{
	class Customer
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public byte[] PasswordHash { get; set; }
		public string ContactEmail { get; set; }
		public string ContactPhone { get; set; }
		public string FIO { get; set; }
		public List<ArtOrder> Orders { get; set; }
	}
}

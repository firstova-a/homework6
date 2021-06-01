using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hw6Server.EF
{
	class Art
	{
		public int Id { get; set; }

		public string Path { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		[NotMapped]
		public List<eType> Types { get; set; }
		[NotMapped]
		public List<string> Styles { get; set; }

		public enum eType
		{
			digital,
			traditional,
			sketch,
			portrait,
			backgraund
		}
	}
}

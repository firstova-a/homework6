using System;
using System.Collections.Generic;
using System.Text;

namespace hw6Server.EF
{
	class Type
	{
		/*тип арта: портрет, в полный рост, фон, пейзаж, скетч (в перечислении)*/
		public enum WorkType
		{
			Portrait = 1,
			FullBody,
			Backgroung,
			Landscape,
			Sketch
		}
		public WorkType Id { get; set; }
	}
}

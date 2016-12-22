using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day01 : AdventOfCodeDay
	{
		private List<Vector> Directions { get; set; }

		public Day01()
		{
			var input = File.ReadAllText("../../input/day1.txt");
			Directions = input.Split(',').Select(a => new Vector
			{
				Direction = a.Trim().Substring(0, 1),
				Distance = Convert.ToInt32(a.Trim().Substring(1))
			}).ToList();
		}

		public override object Solve1()
		{
			var lx = 0;
			var ly = 0;
			var d = 0;


			foreach (var v in Directions)
			{
				switch (v.Direction)
				{
					case "R":
						d = (d + 1 + 4) % 4;
						break;
					case "L":
						d = (d - 1 + 4) % 4;
						break;
				}

				switch (d)
				{
					case 0:
						ly += v.Distance;
						break;
					case 1:
						lx += v.Distance;
						break;
					case 2:
						ly -= v.Distance;
						break;
					case 3:
						lx -= v.Distance;
						break;
				} 
			}

			d = Math.Abs(lx) + Math.Abs(ly);

			return d;
		}

		public override object Solve2()
		{
			var lx = 0;
			var ly = 0;
			var d = 0;

			var locationsVisited = new List<string>();

			foreach (var v in Directions)
			{
				switch (v.Direction)
				{
					case "R":
						d = (d + 1 + 4) % 4;
						break;
					case "L":
						d = (d - 1 + 4) % 4;
						break;
				}

				var oldx = lx;
				var oldy = ly;

				switch (d)
				{
					case 0:
						ly += v.Distance;
						break;
					case 1:
						lx += v.Distance;
						break;
					case 2:
						ly -= v.Distance;
						break;
					case 3:
						lx -= v.Distance;
						break;
				}

				bool goingx = false;


				int start = 0;
				int finish = 0;

				if (oldx != lx)
				{
					goingx = true;
					start = oldx;
					finish = lx;
				}
				else if (oldy != ly)
				{
					start = oldy;
					finish = ly;
				}

				if (start > finish)
				{
					for (int i = start; i > finish; i--)
					{
						locationsVisited.Add(goingx ? $"{i},{ly}" : $"{lx},{i}");
					}
				}
				else
				{
					for (int i = start; i < finish; i++)
					{
						locationsVisited.Add(goingx ? $"{i},{ly}" : $"{lx},{i}");
					}
				}

				var found = (from p in locationsVisited
							 group p by p into g
							 select new { p = g.Key, c = g.Count() }).Where(p => p.c == 2).FirstOrDefault();

				if (found != null)
				{
					return found.p.Split(',').Select(a => Math.Abs(Convert.ToInt32(a))).Sum();
				}

			}
			 
			return d;
		}

		public class Vector
		{
			public string Direction { get; set; }
			public int Distance { get; set; }
		}
	}
}

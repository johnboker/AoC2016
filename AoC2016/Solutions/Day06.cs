using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day06 : AdventOfCodeDay
	{
		public List<char[]> Message { get; set; }
		public Day06()
		{
			var input = File.ReadLines("../../input/day6.txt");
			Message = input.Select(a => a.ToCharArray()).ToList();
		}

		public override object Solve1()
		{
			var msg = "";
			var lineLength = Message.First().Count();

			for (var i = 0; i < lineLength; i++)
			{
				var chr = (from c in Message.Select(a => a[i])
						   group c by c into grp
						   orderby grp.Count() descending
						   select new { Character = grp.Key, Count = grp.Count() }).First().Character;

				msg += chr;
				
			}

			return msg;
		}

		public override object Solve2()
		{
			var msg = "";
			var lineLength = Message.First().Count();

			for (var i = 0; i < lineLength; i++)
			{
				var chr = (from c in Message.Select(a => a[i])
						   group c by c into grp
						   orderby grp.Count() ascending
						   select new { Character = grp.Key, Count = grp.Count() }).First().Character;

				msg += chr;

			}

			return msg;
		}
	}
}

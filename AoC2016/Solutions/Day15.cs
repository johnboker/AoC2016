using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day15 : AdventOfCodeDay
	{
		List<Arrangement> Arrangements { get; set; }
		public Day15()
		{
			
		}

		public override object Solve1()
		{
			var input = File.ReadLines("../../input/day15-part1.txt");
			Arrangements = input.Select(l => new Arrangement(l)).ToList();

			var i = 0;

			while (CheckPositions(i) != true)
			{
				i++;
			}

			return i - 1;
		}


		public override object Solve2()
		{
			var input = File.ReadLines("../../input/day15-part2.txt");
			Arrangements = input.Select(l => new Arrangement(l)).ToList();

			var i = 0;

			while (CheckPositions(i) != true)
			{
				i++;
			}

			return i - 1;
		}

		public bool CheckPositions(int time)
		{
			return Arrangements.All(a => a.PositionAtTime(time + (a.DiskNumber - 1)) == 0);
		}
	}

	public class Arrangement
	{
		public int DiskNumber { get; set; }
		public int Positions { get; set; }
		public int InitialPosition { get; set; }
		public int Time { get; set; }

		public Arrangement(string def)
		{
			var pattern = new Regex(@"Disc #(?<A>\d+) has (?<B>\d+) positions; at time=(?<C>\d+), it is at position (?<D>\d+).");
			var matches = pattern.Match(def);
			DiskNumber = Convert.ToInt32(matches.Groups["A"].Value);
			Positions = Convert.ToInt32(matches.Groups["B"].Value);
			Time = Convert.ToInt32(matches.Groups["C"].Value);
			InitialPosition = Convert.ToInt32(matches.Groups["D"].Value);
		}

		public int PositionAtTime(int time)
		{
			return (InitialPosition + time) % Positions;
		}
	}
}

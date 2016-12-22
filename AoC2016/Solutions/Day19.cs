using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day19 : AdventOfCodeDay
	{
		public int Input = 3014603;
		public Day19()
		{
		}

		public override object Solve1()
		{
			var elfs = new Queue<Elf>();
			for (int i = 0; i < Input; i++)
			{
				elfs.Enqueue(new Elf(i + 1));
			}

			while (elfs.Count() > 1)
			{
				var elf1 = elfs.Dequeue();
				var elf2 = elfs.Dequeue();
				elf1.PresentCount += elf2.PresentCount;
				elfs.Enqueue(elf1);
			}

			return elfs.First().Index;
		}

		public override object Solve2()
		{
			// My solution was taking too long, this one is better :)
			//https://www.reddit.com/r/adventofcode/comments/5j4lp1/2016_day_19_solutions/dbdihvu/

			var n = Input;
			int pow = (int)Math.Floor(Math.Log(n) / Math.Log(3));
			int b = (int)Math.Pow(3, pow);
			if (n == b)
				return n;
			if (n - b <= b)
				return n - b;
			return 2 * n - 3 * b;
		}
	}


	public class Elf
	{
		public Elf(int index)
		{
			Index = index;
			PresentCount = 1;
		}
		public int PresentCount { get; set; }
		public int Index { get; set; }
	}
}

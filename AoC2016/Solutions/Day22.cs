using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day22 : AdventOfCodeDay
	{
		public List<Node> Nodes { get; set; }
		public Day22()
		{
			Nodes = File.ReadLines("../../input/day22.txt")
						.Select(a => new Node(a))
						.OrderBy(a => a.Y).ThenBy(a => a.X)
						.ToList();
		}

		public override object Solve1()
		{
			var data = from n1 in Nodes
					   from n2 in Nodes
					   where n1 != n2 && n1.Used <= n2.Available && n1.Used != 0
					   select new { n1, n2 };

			return data.Count();
		}

		public override object Solve2()
		{
			Print();
			// so i counted this.
			// 15 up, 2 left, 7 up, 6 right, 5 * (xmax-1) + 1
			return 15 + 2 + 7 + 6 + (5 * (Nodes.Max(a => a.X) - 1)) + 1;
		}

		public void Print()
		{
			var emptyNode = Nodes.FirstOrDefault(a => a.Used == 0);
			var ymax = Nodes.Max(n => n.Y);
			var xmax = Nodes.Max(n => n.X);
			for (int x = 0; x < xmax + 1; x++)
			{
				Console.Write($" {x:00}");
			}
			Console.WriteLine();
			int i = 0;
			for (int y = 0; y < ymax + 1; y++)
			{
				for (int x = 0; x < xmax + 1; x++)
				{
					var n = Nodes[i];
					if (n.Used == 0)
					{
						Console.Write(" _ ");
					}
					else if (n.Used > emptyNode.Size)
					{
						Console.Write(" # ");
					}
					else
					{
						Console.Write(" . ");
					}
					i++;
				}
				Console.Write($" {(i / (xmax + 1)) - 1}");
				Console.WriteLine();
			}
		}
	}

	public class Node
	{
		private static Regex NodeLocationPattern = new Regex(@"/dev/grid/node-x(?<X>\d+)-y(?<Y>\d+)");

		public int X { get; set; }
		public int Y { get; set; }
		public int Size { get; set; }
		public int Used { get; set; }
		public int Available => Size - Used;

		public Node(string line)
		{
			var parts = line.Split(' ').Where(a => !string.IsNullOrWhiteSpace(a)).ToList();
			var matches = NodeLocationPattern.Match(parts[0]);
			X = int.Parse(matches.Groups["X"].Value);
			Y = int.Parse(matches.Groups["Y"].Value);
			Size = int.Parse(parts[1].TrimEnd('T'));
			Used = int.Parse(parts[2].TrimEnd('T'));
		}

		public override string ToString()
		{
			return string.Format("[Node: X={0}, Y={1}, Size={2}, Used={3}, Available={4}]", X, Y, Size, Used, Available);
		}

		public override int GetHashCode()
		{
			return $"{X}, {Y}".GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			return ((Node)(obj)).GetHashCode() == GetHashCode();
		}
	}
}

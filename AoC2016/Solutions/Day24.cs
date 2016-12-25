using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day24 : AdventOfCodeDay
	{
		public char[,] Map { get; set; }
		public int ColumnCount { get; set; }
		public int RowCount { get; set; }

		public Day24()
		{
			var input = File.ReadLines("../../input/day24.txt").ToList();
			ColumnCount = input.First().Count() - 2;
			RowCount = input.Count() - 2;
			Map = new char[ColumnCount, RowCount];

			for (var row = 1; row < RowCount + 1; row++)
			{
				for (var col = 1; col < ColumnCount + 1; col++)
				{
					Map[col - 1, row - 1] = input[row][col];
				}
			}
		}

		public override object Solve1()
		{
			var locations = new List<Location>();

			for (var row = 0; row < RowCount; row++)
			{
				for (var col = 0; col < ColumnCount; col++)
				{
					if (Map[col, row] >= '0' && Map[col, row] <= '9')
					{
						locations.Add(new Location(row, col, Map[col, row]));
					}
				}
			}

			locations = locations.OrderBy(a => a.Value).ToList();

			var distances = new int[locations.Count(), locations.Count()];

			foreach (var loc1 in locations)
			{
				Console.Write($" \t{loc1.Value}");
			}

			Console.WriteLine();
			// find shortest distance between locations 
			foreach (var loc1 in locations)
			{
				Console.Write($"{loc1.Value} ");
				foreach (var loc2 in locations)
				{
					var dist = 0;

					if (loc1 != loc2)
					{
						dist = GetShortestDistance(loc2, new Move { Column = loc1.Column, Row = loc1.Row });
					}

					distances[loc1.IntValue, loc2.IntValue] = dist;

					Console.Write($"\t{dist}");
				}
				Console.WriteLine();
			}

			var permutations = Utilities.GeneratePermutations(locations).Where(a => a.First().IntValue == 0);
			var d2 = int.MaxValue;
			//List<Location> shortest = null;
			foreach (var p in permutations)
			{
				int d = 0;
				for (int i = 0; i < p.Count() - 1; i++)
				{
					d += distances[p[i].IntValue, p[i + 1].IntValue];
				}
				if (d < d2)
				{
					//shortest = p.ToList();
					d2 = d;
				}
			} 

			return d2;
		}

		public override object Solve2()
		{
			var locations = new List<Location>();

			for (var row = 0; row < RowCount; row++)
			{
				for (var col = 0; col < ColumnCount; col++)
				{
					if (Map[col, row] >= '0' && Map[col, row] <= '9')
					{
						locations.Add(new Location(row, col, Map[col, row]));
					}
				}
			}

			locations = locations.OrderBy(a => a.Value).ToList();
			var location0 = locations[0];

			var distances = new int[locations.Count(), locations.Count()];

			foreach (var loc1 in locations)
			{
				Console.Write($" \t{loc1.Value}");
			}

			Console.WriteLine();
			// find shortest distance between locations 
			foreach (var loc1 in locations)
			{
				Console.Write($"{loc1.Value} ");
				foreach (var loc2 in locations)
				{
					var dist = 0;

					if (loc1 != loc2)
					{
						dist = GetShortestDistance(loc2, new Move { Column = loc1.Column, Row = loc1.Row });
					}

					distances[loc1.IntValue, loc2.IntValue] = dist;

					Console.Write($"\t{dist}");
				}
				Console.WriteLine();
			}

			var permutations = Utilities.GeneratePermutations(locations).Where(a => a.First().IntValue == 0);
			var d2 = int.MaxValue;
			//List<Location> shortest = null;
			foreach (var p in permutations)
			{
				int d = 0;
				p.Add(location0);
				for (int i = 0; i < p.Count() - 1; i++)
				{
					d += distances[p[i].IntValue, p[i + 1].IntValue];
				}
				if (d < d2)
				{
					//shortest = p.ToList();
					d2 = d;
				}
			} 

			return d2;
		}

		public int GetShortestDistance(Location loc2, Move start)
		{
			var queue = new Queue<Move>();
			var visited = new HashSet<Move>();
			queue.Enqueue(start);
			int steps = 0;

			var map = new char[ColumnCount, RowCount];
			Array.Copy(Map, map, ColumnCount * RowCount);

			var paths = new List<List<Move>>();

			while (queue.Any())
			{
				var move = queue.Dequeue();
				map[move.Column, move.Row] = 'X';
				if (move.Row == loc2.Row && move.Column == loc2.Column)
				{
					paths.Add(GetPath(move));
				}

				if (move.Column + 1 < ColumnCount && isValidMove(Map[move.Column + 1, move.Row]))
				{
					var m = new Move { Row = move.Row, Column = move.Column + 1, Parent = move };
					if (!visited.Contains(m))
					{
						queue.Enqueue(m);
						visited.Add(m);
					}
				}

				if (move.Column - 1 >= 0 && isValidMove(Map[move.Column - 1, move.Row]))
				{
					var m = new Move { Row = move.Row, Column = move.Column - 1, Parent = move };
					if (!visited.Contains(m))
					{
						queue.Enqueue(m);
						visited.Add(m);
					}
				}

				if (move.Row + 1 < RowCount && isValidMove(Map[move.Column, move.Row + 1]))
				{
					var m = new Move { Row = move.Row + 1, Column = move.Column, Parent = move };
					if (!visited.Contains(m))
					{
						queue.Enqueue(m);
						visited.Add(m);
					}
				}

				if (move.Row - 1 >= 0 && isValidMove(Map[move.Column, move.Row - 1]))
				{
					var m = new Move { Row = move.Row - 1, Column = move.Column, Parent = move };
					if (!visited.Contains(m))
					{
						queue.Enqueue(m);
						visited.Add(m);
					}
				}

				steps++;
			}

			return paths.Min(a => a.Count()) - 1;
		}

		private bool isValidMove(char c)
		{
			return (c >= '0' && c <= '9') || c == '.';
		}

		public List<Move> GetPath(Move m)
		{
			var path = new List<Move>();
			do
			{
				path.Add(m);
				m = m.Parent;
			}
			while (m != null);
			return path;
		} 

		public class Move
		{
			public Move Parent { get; set; }
			public int Row { get; set; }
			public int Column { get; set; }

			public override bool Equals(object obj)
			{
				var m = (Move)(obj);
				return m.Row == Row && m.Column == Column;
			}

			public override int GetHashCode()
			{
				return Row.GetHashCode() + Column.GetHashCode();
			}
		}

		public class Location
		{
			public Location(int row, int col, char v)
			{ 
				Row = row;
				Column = col;
				Value = v;
			}

			public override bool Equals(object obj)
			{
				var l = (Location)(obj);
				return l.Column == Column && l.Row == Row && l.Value == Value;
			}

			public int IntValue => Value - '0';

			public override int GetHashCode()
			{
				return Row.GetHashCode() + Column.GetHashCode();
			}
			public char Value { get; set; }
			public int Row { get; set; }
			public int Column { get; set; } 
		}
	}
}

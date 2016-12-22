using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day13 : AdventOfCodeDay
	{
		public int FavoriteNumber => 1362;
		public char[,] Floor { get; set; }
		public const int MAX_X = 50; //100;
		public const int MAX_Y = 50; //100;
		public const int FINAL_X = 31;
		public const int FINAL_Y = 39;


		public override object Solve1()
		{
			Floor = new char[MAX_Y, MAX_X];
			for (var y = 0; y < MAX_Y; y++)
			{
				for (var x = 0; x < MAX_X; x++)
				{
					var isOpen = IsOpenSpace(x, y);
					Floor[y, x] = isOpen ? ' ' : '\u2588';
				}
			}
			var pathMoves = new List<int>();
			FindPath(1, 1, 0, pathMoves);

			return pathMoves.Min();
		}

		public override object Solve2()
		{
			Floor = new char[MAX_Y, MAX_X];
			for (var y = 0; y < MAX_Y; y++)
			{
				for (var x = 0; x < MAX_X; x++)
				{
					var isOpen = IsOpenSpace(x, y);
					Floor[y, x] = isOpen ? ' ' : '\u2588';
				}
			}
			var locations = new HashSet<string>();
			FindLocations(1, 1, 0, locations);

			return locations.Count();
		}


		public void PrintFloor()
		{
			for (var y = 0; y < MAX_Y; y++)
			{
				for (var x = 0; x < MAX_X; x++)
				{
					Console.ForegroundColor = Floor[y, x] == '.' ? ConsoleColor.Red : ConsoleColor.Black;
					Console.Write(Floor[y, x]);
				}
				Console.WriteLine();
			}
		}


		public int FindLocations(int y, int x, int moves, HashSet<string> locations)
		{
			locations.Add($"({x},{y}");

			if (moves == 50)
			{
				return -1;
			}

			int m = -1;

			if (y + 1 < MAX_Y && Floor[y + 1, x] != '\u2588' && Floor[y + 1, x] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindLocations(y + 1, x, moves + 1, locations);
			}

			if (x + 1 < MAX_X && Floor[y, x + 1] != '\u2588' && Floor[y, x + 1] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindLocations(y, x + 1, moves + 1, locations);
			}

			if (y - 1 >= 0 && Floor[y - 1, x] != '\u2588' && Floor[y - 1, x] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindLocations(y - 1, x, moves + 1, locations);
			}

			if (x - 1 >= 0 && Floor[y, x - 1] != '\u2588' && Floor[y, x - 1] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindLocations(y, x - 1, moves + 1, locations);
			}

			if (m > -1) return m;

			Floor[y, x] = ' ';

			return -1;
		}

		public int FindPath(int y, int x, int moves, List<int> foundPathMoves)
		{
			if (y == FINAL_Y && x == FINAL_X && (foundPathMoves.Count == 0 || moves < foundPathMoves.Min()))
			{
				foundPathMoves.Add(moves);
				Console.WriteLine($"Moves: {moves}");
				PrintFloor();
				Console.WriteLine();
			}

			int m = -1;

			if (y + 1 < MAX_Y && Floor[y + 1, x] != '\u2588' && Floor[y + 1, x] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindPath(y + 1, x, moves + 1, foundPathMoves);
			}

			if (x + 1 < MAX_X && Floor[y, x + 1] != '\u2588' && Floor[y, x + 1] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindPath(y, x + 1, moves + 1, foundPathMoves);
			}

			if (y - 1 >= 0 && Floor[y - 1, x] != '\u2588' && Floor[y - 1, x] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindPath(y - 1, x, moves + 1, foundPathMoves);
			}

			if (x - 1 >= 0 && Floor[y, x - 1] != '\u2588' && Floor[y, x - 1] != '.' && m == -1)
			{
				Floor[y, x] = '.';
				m = FindPath(y, x - 1, moves + 1, foundPathMoves);
			}

			if (m > -1) return m;

			Floor[y, x] = ' ';

			return -1;
		}

		public bool IsOpenSpace(int x, int y)
		{
			var n = ((x * x) + (3 * x) + (2 * x * y) + y + (y * y)) + FavoriteNumber;
			return NumberOfSetBits(n) % 2 == 0;
		}

		public int NumberOfSetBits(int i)
		{
			i = i - ((i >> 1) & 0x55555555);
			i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
			return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}
	}
}

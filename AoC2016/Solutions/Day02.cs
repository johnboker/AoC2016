using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day02 : AdventOfCodeDay
	{
		private List<char[]> Instructions { get; set; }
		private char[,] Keypad1 = new char[3, 3] { { '1', '2', '3'},
												   { '4', '5', '6'},
												   { '7', '8', '9'}};

		private char[,] Keypad2 = new char[5, 5] { { ' ', ' ', '1', ' ', ' '},
												  { ' ', '2', '3', '4', ' ' },
												  { '5', '6', '7', '8', '9' },
												  { ' ', 'A', 'B', 'C', ' ' },
												  { ' ', ' ', 'D', ' ', ' '}};
		public Day02()
		{
			var input = File.ReadLines("../../input/day2.txt");
			Instructions = input.Select(a => a.ToArray()).ToList();
		}



		public override object Solve1()
		{
			return Solve(Keypad1, 3, 0, 0);
		}

		public override object Solve2()
		{
			return Solve(Keypad2, 5, 0, 2);
		}



		public string Solve(char[,] keypad, int n, int startRow, int startCol)
		{
			var code = "";
			 
			int r = 0;
			int c = 2;

			for (int i = 0; i < Instructions.Count; i++)
			{
				for (int j = 0; j < Instructions[i].Length; j++)
				{
					var instruction = Instructions[i][j];

					//Console.Write(keypad[r, c]);

					if (instruction == 'U' && c > 0 && keypad[r, c - 1] != ' ')
					{
						c--;
					}
					if (instruction == 'D' && c < n - 1 && keypad[r, c + 1] != ' ')
					{
						c++;
					}
					if (instruction == 'L' && r > 0 && keypad[r - 1, c] != ' ')
					{
						r--;
					}
					if (instruction == 'R' && r < n - 1 && keypad[r + 1, c] != ' ')
					{
						r++;
					}
				}
				//Console.WriteLine();
				code += keypad[c, r];
			}

			return code;
		}
	}
}

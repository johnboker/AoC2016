using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day08 : AdventOfCodeDay
	{
		public const int SCREEN_X = 50;
		public const int SCREEN_Y = 6;
		public int[,] Screen { get; set; }
		public List<Command> Commands { get; set; }

		public Day08()
		{
			Screen = new int[SCREEN_Y, SCREEN_X];
			Commands = File.ReadLines("../../input/day8.txt").Select(a => new Command(a)).ToList();
		}

		public override object Solve1()
		{
			Print();
			Console.WriteLine();
			foreach (var command in Commands)
			{
				Console.WriteLine(command.CommandString);
				switch (command.Instruction)
				{
					case Instruction.RECT:
						Rect(command);
						break;
					case Instruction.ROTATE_COLUMN:
						RotateColumn(command);
						break;
					case Instruction.ROTATE_ROW:
						RotateRow(command);
						break;
				}
				Print();
				Console.WriteLine(Screen.Cast<int>().Sum(a => a));
			}
			return Screen.Cast<int>().Sum(a => a);
		}

		public void Print()
		{
			for (int y = 0; y < SCREEN_Y; y++)
			{
				for (int x = 0; x < SCREEN_X; x++)
				{
					Console.Write(Screen[y, x] == 0 ? ' ' : '#');
				}
				Console.WriteLine();
			}
		}

		public void RotateRow(Command command)
		{
			var tempRow = new List<int>();
			for (var x = 0; x < SCREEN_X; x++)
			{
				tempRow.Add(Screen[command.Y, x]);
			}
			Rotate(tempRow, command.R);
			for (var x = 0; x < SCREEN_X; x++)
			{
				Screen[command.Y, x] = tempRow[x];
			}
		}

		public void RotateColumn(Command command)
		{
			var tempColumn = new List<int>();
			for (var y = 0; y < SCREEN_Y; y++)
			{
				tempColumn.Add(Screen[y, command.X]);
			}
			Rotate(tempColumn, command.R);
			for (var y = 0; y < SCREEN_Y; y++)
			{
				Screen[y, command.X] = tempColumn[y];
			}
		}

		public void Rotate(List<int> list, int r)
		{
			for (int i = 0; i < r; i++)
			{
				var last = list.Last();
				list.RemoveAt(list.Count - 1);
				list.Insert(0, last);
			}
		}

		public void Rect(Command command)
		{
			for (var y = 0; y < command.Y; y++)
			{
				for (var x = 0; x < command.X; x++)
				{
					Screen[y, x] = 1;
				}
			}
		}

		public override object Solve2()
		{
			return null;
		}
	}

	public class Command
	{
		public Command(string command)
		{
			CommandString = command;
			Parse();
		}

		public string CommandString { get; set; }
		public Instruction Instruction { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int R { get; set; }

		public void Parse()
		{
			if (CommandString.StartsWith("rect", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rect (?<X>\d+)x(?<Y>\d+)");
				var match = pattern.Match(CommandString);
				X = int.Parse(match.Groups["X"].Value);
				Y = int.Parse(match.Groups["Y"].Value);
				Instruction = Instruction.RECT;
			}
			else if (CommandString.StartsWith("rotate row", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rotate row y=(?<Y>\d+) by (?<R>\d+)");
				var match = pattern.Match(CommandString);
				Y = int.Parse(match.Groups["Y"].Value);
				R = int.Parse(match.Groups["R"].Value);
				Instruction = Instruction.ROTATE_ROW;
			}
			else if (CommandString.StartsWith("rotate column", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rotate column x=(?<X>\d+) by (?<R>\d+)");
				var match = pattern.Match(CommandString);
				X = int.Parse(match.Groups["X"].Value);
				R = int.Parse(match.Groups["R"].Value);
				Instruction = Instruction.ROTATE_COLUMN;
			}
		}
	}

	public enum Instruction
	{
		RECT,
		ROTATE_ROW,
		ROTATE_COLUMN
	}
}

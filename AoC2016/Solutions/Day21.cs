using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day21 : AdventOfCodeDay
	{
		public string Password = "abcdefgh";
		public string ScrambledPassword = "fbgdceah";
		public List<Operation> Operations { get; set; }

		public Day21()
		{
			Operations = File.ReadLines("../../input/day21.txt").Select(a => new Operation(a)).ToList();
		}

		public override object Solve1()
		{
			return Scramble(Password);
		}

		public override object Solve2()
		{
			return UnScramble(ScrambledPassword);
		}

		public string UnScramble(string p)
		{
			var permuations = Utilities.GeneratePermutations(p.ToList()).Select(a => string.Join("", a)).ToList();
			foreach (var permutation in permuations)
			{
				if (Scramble(permutation) == p) return permutation;
			}
			return "-none found-";
		}

		private string Scramble(string p)
		{
			foreach (var o in Operations)
			{
				p = o.ApplyTo(p);
			}
			return p;
		}
	}

	public class Operation
	{
		public Operation() { }
		public Operation(string line)
		{
			Operands = new List<object>();

			if (line.StartsWith("rotate right", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rotate right (?<A>\d+) step(\w*)");
				var matches = pattern.Match(line);

				MyCommand = Command.RotateRight;
				Operands.Add(int.Parse(matches.Groups["A"].Value));
			}
			else if (line.StartsWith("rotate left", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rotate left (?<A>\d+) step(\w*)");
				var matches = pattern.Match(line);

				MyCommand = Command.RotateLeft;
				Operands.Add(int.Parse(matches.Groups["A"].Value));
			}
			else if (line.StartsWith("swap letter", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"swap letter (?<A>\w) with letter (?<B>\w)");
				var matches = pattern.Match(line);

				MyCommand = Command.SwapLetter;
				Operands.Add(matches.Groups["A"].Value);
				Operands.Add(matches.Groups["B"].Value);
			}
			else if (line.StartsWith("move position", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"move position (?<A>\d+) to position (?<B>\d+)");
				var matches = pattern.Match(line);

				MyCommand = Command.Move;
				Operands.Add(int.Parse(matches.Groups["A"].Value));
				Operands.Add(int.Parse(matches.Groups["B"].Value));
			}
			else if (line.StartsWith("rotate based", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"rotate based on position of letter (?<A>\w)");
				var matches = pattern.Match(line);

				MyCommand = Command.RotateBased;
				Operands.Add(matches.Groups["A"].Value);
			}
			else if (line.StartsWith("swap position", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"swap position (?<A>\d+) with position (?<B>\d+)");
				var matches = pattern.Match(line);

				MyCommand = Command.SwapPosition;
				Operands.Add(int.Parse(matches.Groups["A"].Value));
				Operands.Add(int.Parse(matches.Groups["B"].Value));
			}
			else if (line.StartsWith("reverse positions", StringComparison.Ordinal))
			{
				var pattern = new Regex(@"reverse positions (?<A>\d+) through (?<B>\d+)");
				var matches = pattern.Match(line);

				MyCommand = Command.ReversePosition;
				Operands.Add(int.Parse(matches.Groups["A"].Value));
				Operands.Add(int.Parse(matches.Groups["B"].Value));
			}
		}

		public Command MyCommand { get; set; }
		public List<object> Operands { get; set; }

		public enum Command
		{
			RotateRight,
			RotateLeft,
			RotateBased,
			SwapLetter,
			SwapPosition,
			Move,
			ReversePosition
		}

		public override string ToString()
		{
			return string.Format("[Operation: MyCommand={0}, Operands={1}]", MyCommand, string.Join(",", Operands));
		}

		public string ApplyTo(string password)
		{
			switch (MyCommand)
			{
				case Command.SwapPosition:
					password = SwapPositions(password, (int)Operands[0], (int)Operands[1]);
					break;
				case Command.SwapLetter:
					password = SwapLetter(password, ((string)Operands[0])[0], ((string)Operands[1])[0]);
					break;
				case Command.ReversePosition:
					password = ReversePositions(password, (int)Operands[0], (int)Operands[1]);
					break;
				case Command.RotateLeft:
					password = RotateLeft(password, (int)Operands[0]);
					break;
				case Command.RotateRight:
					password = RotateRight(password, (int)Operands[0]);
					break;
				case Command.Move:
					password = MovePosition(password, (int)Operands[0], (int)Operands[1]);
					break;
				case Command.RotateBased:
					password = RotateBased(password, ((string)Operands[0])[0]);
					break;
			}

			return password;
		}

		private static string SwapPositions(string s, int x, int y)
		{
			var a = s.ToArray();
			var t = a[x];
			a[x] = a[y];
			a[y] = t;
			return string.Join("", a);
		}

		private static string SwapLetter(string s, char a, char b)
		{
			var x = s.IndexOf(a);
			var y = s.IndexOf(b);
			return SwapPositions(s, x, y);
		}

		private static string ReversePositions(string s, int x, int y)
		{
			if (x > y)
			{
				var t = x;
				x = y;
				y = t;
			}

			double len = y - x;

			for (int i = 0; i < Math.Ceiling(len / 2); i++)
			{
				s = SwapPositions(s, i + x, (int)(len - i + x));
			}

			return s;
		}

		private static string RotateLeft(string s, int steps)
		{
			var a = s.ToList();
			for (int i = 0; i < steps; i++)
			{
				var c = a[0];
				a.RemoveAt(0);
				a.Add(c);
			}
			return string.Join("", a);
		}


		private static string RotateRight(string s, int steps)
		{
			var a = s.ToList();
			for (int i = 0; i < steps; i++)
			{
				var c = a[s.Length - 1];
				a.RemoveAt(s.Length - 1);
				a.Insert(0, c);
			}
			return string.Join("", a);
		}

		private static string MovePosition(string s, int x, int y)
		{
			var a = s.ToList();
			var c = a[x];
			a.RemoveAt(x);
			a.Insert(y, c);
			return string.Join("", a);
		}

		private string RotateBased(string s, char a)
		{
			var idx = s.IndexOf(a);
			var steps = idx + 1 + (idx >= 4 ? 1 : 0);
			return RotateRight(s, steps);
		}
	}
}

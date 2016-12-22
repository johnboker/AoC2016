using System;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions
{
	public class Day16 : AdventOfCodeDay
	{
		public string Input = "11011110011011101";
		public Day16()
		{
		}

		public override object Solve1()
		{
			var inp = Input;

			while (inp.Length < 272)
			{
				inp = Translate(inp);
			}

			inp = inp.Substring(0, 272);
			return Checksum(inp);
		}

		public override object Solve2()
		{
			var inp = Input;

			while (inp.Length < 35651584)
			{
				inp = Translate(inp);
			}

			inp = inp.Substring(0, 35651584);
			return Checksum(inp);
		}

		public string Translate(string a)
		{
			var b = a.Reverse().ToArray();
			for (var i = 0; i < b.Length; i++)
			{
				b[i] = b[i] == '0' ? '1' : '0';
			}
			return $"{a}0{new string(b)}";
		}

		public string Checksum(string a)
		{
			var sb = new StringBuilder();
			for (var i = 0; i < a.Length; i += 2)
			{
				var c1 = a[i];
				var c2 = a[i + 1];
				sb.Append(c1 == c2 ? '1' : '0');
			}
			var s = sb.ToString();
			if (s.Length % 2 == 1) return s;
			return Checksum(s);
		}
	}
}

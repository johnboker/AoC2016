using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace AoC2016.Solutions
{
	public class Day17 : AdventOfCodeDay
	{
		private string Input = "ioramepc";
		private MD5 Md5 = MD5.Create();
		private List<string> PathsFound { get; set; }

		public override object Solve1()
		{
			PathsFound = new List<string>();
			FindPath("", 0, 0);
			return PathsFound.OrderBy(a => a.Length).First();
		}

		public override object Solve2()
		{
			if (PathsFound == null) Solve1();
			return PathsFound.Max(a => a.Length);
		}

		public void FindPath(string path, int r, int c)
		{
			if (r == 3 && c == 3)
			{
				PathsFound.Add(path);
				return;
			}

			var hash = Utilities.GetMd5Hash(Md5, Input + path).Substring(0, 4);
			var moves = FindNextMoves(hash, r, c);

			foreach (var m in moves)
			{
				if (m == 'U') FindPath(path + 'U', r - 1, c);
				if (m == 'D') FindPath(path + 'D', r + 1, c);
				if (m == 'L') FindPath(path + 'L', r, c - 1);
				if (m == 'R') FindPath(path + 'R', r, c + 1);
			}
		}

		private List<char> FindNextMoves(string hash, int currentRow, int currentCol)
		{
			var moves = new List<char>();

			if (IsOpen(hash[0]) && currentRow > 0) moves.Add('U');
			if (IsOpen(hash[1]) && currentRow < 3) moves.Add('D');
			if (IsOpen(hash[2]) && currentCol > 0) moves.Add('L');
			if (IsOpen(hash[3]) && currentCol < 3) moves.Add('R');

			return moves;
		}

		public bool IsOpen(char c)
		{
			if ((c >= '0' && c <= '9') || c == 'a') return false;
			return true;
		}

	}
}

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions
{
	public class Day18 : AdventOfCodeDay
	{
		public string Input = "^..^^.^^^..^^.^...^^^^^....^.^..^^^.^.^.^^...^.^.^.^.^^.....^.^^.^.^.^.^.^.^^..^^^^^...^.....^....^.";

		public override object Solve1()
		{
			return CountSafeTilesForRows(40);
		}

		public override object Solve2()
		{
			return CountSafeTilesForRows(400000);
		}

		public int CountSafeTilesForRows(int rows)
		{
			var row = Input;
			int count = row.Count(a => a == '.');
			for (var i = 0; i < rows - 1; i++)
			{
				row = GenerateNextRow(row);
				count += row.Count(a => a == '.');
			}
			return count;
		}

		public string GenerateNextRow(string row)
		{
			var nextRow = new StringBuilder();

			for (var i = 0; i < row.Length; i++)
			{
				var left = i > 0 ? row[i - 1] : '.';
				var center = row[i];
				var right = i < row.Length - 1 ? row[i + 1] : '.'; 
				nextRow.Append(left == right ? '.' : '^'); 
			}
			return nextRow.ToString();
		}
	}
}

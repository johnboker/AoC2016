using System;
using System.IO;
using System.Text;

namespace AoC2016.Solutions
{
	public class Day09 : AdventOfCodeDay
	{
		public string Input { get; set; }
		public Day09()
		{
			Input = File.ReadAllText("../../input/day9.txt");
		}

		public override object Solve1()
		{
			var decompressed = Decompress(Input);
			return decompressed.Length;
		}

		public override object Solve2()
		{
			var count = DecompressCount(Input, 0); 
			return count;
		}

		public string Decompress(string compressed)
		{
			var decompressed = new StringBuilder();

			for (int i = 0; i < compressed.Length; i++)
			{
				var c = compressed[i];
				if (c == '(')
				{
					var end = compressed.IndexOf(')', i);
					var markerLength = end + 1 - i;
					var markerString = compressed.Substring(i, markerLength);
					var marker = new Marker(markerString);

					string repeatString = compressed.Substring(i + markerLength, marker.Count); 

					for (int r = 0; r < marker.Repeat; r++)
					{
						decompressed.Append(repeatString);
					}

					i = i + markerLength + marker.Count - 1;
				}
				else
				{
					decompressed.Append(c);
				}
			} 
			return decompressed.ToString();
		}

		public ulong DecompressCount(string compressed, int recurseCount)
		{
			ulong count = 0;

			recurseCount++;

			for (int i = 0; i < compressed.Length; i++)
			{
				var c = compressed[i];
				if (c == '(')
				{
					var end = compressed.IndexOf(')', i);
					var markerLength = end + 1 - i;
					var markerString = compressed.Substring(i, markerLength);
					var marker = new Marker(markerString); 
					string repeatString = compressed.Substring(i + markerLength, marker.Count);
					var containsParen = repeatString.Contains("("); 
					count += (containsParen ? DecompressCount(repeatString, recurseCount) : (ulong)repeatString.Length) * (ulong)marker.Repeat; 
					i = i + markerLength + marker.Count - 1;
				}
				else
				{
					count++;
				}
			} 
			return count;
		}
	}

	public class Marker
	{
		public Marker(string marker)
		{
			//Console.WriteLine(marker);
			var parts = marker.Substring(1, marker.Length - 2).Split('x');
			Count = Convert.ToInt32(parts[0]);
			Repeat = Convert.ToInt32(parts[1]);
		}

		public int Count { get; set; }
		public int Repeat { get; set; } 
	}
}

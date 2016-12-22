using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day20 : AdventOfCodeDay
	{
		List<IpRange> BlockedIpRanges { get; set; }
		public Day20()
		{
			BlockedIpRanges = File.ReadLines("../../input/day20.txt").Select(a => new IpRange(a)).OrderBy(a => a.Start).ThenBy(a => a.End).ToList();
		}

		public override object Solve1()
		{
			var newLength = 0;
			var length = 0;
			do
			{
				length = BlockedIpRanges.Count();
				int i = 0;
				while (i < length - 1)
				{
					var ip1 = BlockedIpRanges[i];
					var ip2 = BlockedIpRanges[i + 1];
					if (IpRange.IsOverlapping(ip1, ip2))
					{
						var ip3 = IpRange.Combine(ip1, ip2);
						BlockedIpRanges.RemoveAt(i);
						BlockedIpRanges.RemoveAt(i);
						BlockedIpRanges.Insert(i, ip3);
						i--;
						length--;
					}
					i++;
				}

				newLength = BlockedIpRanges.Count();
				BlockedIpRanges = BlockedIpRanges.OrderBy(a => a.Start).ThenBy(a => a.End).ToList();
			} while (newLength < length);


			return BlockedIpRanges.First().End + 1;
		}


		public override object Solve2()
		{
			var newLength = 0;
			var length = 0;
			do
			{
				length = BlockedIpRanges.Count();
				int i = 0;
				while (i < length - 1)
				{
					var ip1 = BlockedIpRanges[i];
					var ip2 = BlockedIpRanges[i + 1];
					if (IpRange.IsOverlapping(ip1, ip2))
					{
						var ip3 = IpRange.Combine(ip1, ip2);
						BlockedIpRanges.RemoveAt(i);
						BlockedIpRanges.RemoveAt(i);
						BlockedIpRanges.Insert(i, ip3);
						i--;
						length--;
					}
					i++;
				}

				newLength = BlockedIpRanges.Count();
				BlockedIpRanges = BlockedIpRanges.OrderBy(a => a.Start).ThenBy(a => a.End).ToList();
			} while (newLength < length);

			var open = new List<IpRange>() { new IpRange(BlockedIpRanges.Last().End + 1, 4294967295) };
			for (var i = 0; i < BlockedIpRanges.Count() - 1; i++)
			{
				open.Add(new IpRange(BlockedIpRanges[i].End + 1, BlockedIpRanges[i + 1].Start - 1));
			}


			return open.Select(a=>(long)(a.End-a.Start) + 1).Sum();
		}
	}
}

public class IpRange
{
	public IpRange(string line)
	{
		var parts = line.Split('-');
		Start = ulong.Parse(parts[0]);
		End = ulong.Parse(parts[1]);
	}

	public IpRange(ulong start, ulong end)
	{
		Start = start;
		End = end;
	}

	public ulong Start { get; set; }
	public ulong End { get; set; }

	public static IpRange Combine(IpRange first, IpRange second)
	{
		return new IpRange(Math.Min(first.Start, second.Start), Math.Max(first.End, second.End));
	}

	public static bool IsOverlapping(IpRange first, IpRange second)
	{
		var lst = new List<IpRange> { first, second }.OrderBy(a => a.Start).ThenBy(a => a.End).ToList();
		return (lst[0].Start <= lst[1].End && lst[1].Start <= lst[0].End) || (lst[0].End + 1 == lst[1].Start);
	}
}


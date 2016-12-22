using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day07 : AdventOfCodeDay
	{
		public List<ParsedIpAddress> IpAddresses { get; set; }
		public Day07()
		{
			IpAddresses = File.ReadLines("../../input/day7.txt").Select(a => new ParsedIpAddress(a)).ToList();
		}

		public override object Solve1()
		{
			return IpAddresses.Count(a => a.SupportsTLS());
		}

		public override object Solve2()
		{
			return IpAddresses.Count(a => a.SupportsSSL());
		}
	}

	public class ParsedIpAddress
	{
		public ParsedIpAddress(string ipAddress)
		{
			Parse(ipAddress);
		}

		public List<string> SupernetSequences { get; set; }
		public List<string> HypernetSequences { get; set; }

		private void Parse(string ip)
		{
			SupernetSequences = new List<string>();
			HypernetSequences = new List<string>();

			string part = "";
			bool isHypernetSequence = false;
			for (var i = 0; i < ip.Length; i++)
			{
				char c = ip[i];
				if (c == '[')
				{
					AddPartToList(part, isHypernetSequence);
					part = "";
					isHypernetSequence = true;
					continue;
				}

				if (c == ']')
				{
					AddPartToList(part, isHypernetSequence);
					part = "";
					isHypernetSequence = false;
					continue;
				}

				part += c;
			}

			AddPartToList(part, isHypernetSequence);
		}

		private void AddPartToList(string part, bool isHypernetSequence)
		{
			if (!string.IsNullOrWhiteSpace(part))
			{
				if (isHypernetSequence) HypernetSequences.Add(part);
				else SupernetSequences.Add(part);
			}
		}

		public bool SupportsTLS()
		{
			return SupernetSequences.Any(PartContainsABBA) && !HypernetSequences.Any(PartContainsABBA);
		}

		public bool SupportsSSL()
		{
			var aba = FindAllABA(SupernetSequences);
			var bab = FindAllABA(HypernetSequences);

			var intersection = bab.Intersect(aba.Select(a => $"{a[1]}{a[0]}{a[1]}"));

			return intersection.Any();
		}

		private List<string> FindAllABA(List<string> sequences) 
		{
			var abaList = new List<string>();
			foreach (var part in sequences)
			{
				for (int i = 0; i < part.Length; i++)
				{
					if (3 + i <= part.Length)
					{
						var s = part.Substring(i, 3);
						if (s == string.Join("", s.Reverse()) && s[0] != s[1])
						{
							abaList.Add(s); 
						}
					}
				}
			}
			return abaList;
		}
 

		private bool PartContainsABBA(string part)
		{
			for (int i = 0; i < part.Length; i++)
			{
				if (4 + i <= part.Length)
				{
					var s = part.Substring(i, 4);
					if (s == string.Join("", s.Reverse()) && s[0] != s[1])
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}

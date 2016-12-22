using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AoC2016.Solutions
{
	public class Day14 : AdventOfCodeDay
	{
		public string Salt = "ngcjuoqr";
		public Day14()
		{

		}

		public override object Solve1()
		{ 
			var md5 = MD5.Create();
			List<Tuple<int, string>> OneTimePadKeys = new List<Tuple<int, string>>();
			int i = 0;
			while (OneTimePadKeys.Count < 64)
			{
				var data = $"{Salt}{i}";
				var hash = Utilities.GetMd5Hash(md5, data);
				var c = GetRunCharacter(hash);
				if (c != '-')
				{
					var five = new string(c, 5);
					for (int j = i + 1; j < 1000 + i; j++)
					{
						var data2 = $"{Salt}{j}";
						var hash2 = Utilities.GetMd5Hash(md5, data2);
						if (hash2.Contains(five))
						{
							OneTimePadKeys.Add(new Tuple<int, string>(i, hash));
							break;
						}
					}
				}
				i++;
			}

			return OneTimePadKeys.Last().Item1;
		}

		public override object Solve2()
		{
			var dictionary = new Dictionary<string, string>();
			var md5 = MD5.Create();
			var OneTimePadKeys = new List<Tuple<int, string>>();
			var i = 0;

			while (OneTimePadKeys.Count < 64)
			{
				var data = $"{Salt}{i}";
				var hash = GetStretchedMD5Hash(md5, data, 2017, dictionary);
				var c = GetRunCharacter(hash);
				if (c != '-')
				{
					var five = new string(c, 5);
					for (int j = i + 1; j < 1000 + i; j++)
					{
						var data2 = $"{Salt}{j}";
						var hash2 = GetStretchedMD5Hash(md5, data2, 2017, dictionary);
						if (hash2.Contains(five))
						{
							OneTimePadKeys.Add(new Tuple<int, string>(i, hash));
							break;
						}
					}
				}
				i++;
			}
			return OneTimePadKeys.Last().Item1;
		}
		private static string GetStretchedMD5Hash(MD5 md5, string input, int n,   Dictionary<string, string> dictionary)
		{
			var h = dictionary.ContainsKey(input) ?  dictionary[input] : string.Empty;
			if (h == string.Empty)
			{
				var key = input;
				for (int i = 0; i < n; i++)
				{
					input = Utilities.GetMd5Hash(md5, input);
				}
				dictionary.Add(key, input);
				return input;
			}
			return h;
		}

		private static char GetRunCharacter(string hash)
		{
			for (int i = 0; i < hash.Length - 2; i++)
			{
				if (hash[i] == hash[i + 1] && hash[i + 1] == hash[i + 2]) return hash[i];
			}
			return '-';
		}
	}
}

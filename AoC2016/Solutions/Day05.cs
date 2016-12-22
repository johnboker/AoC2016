using System;
using System.Security.Cryptography;

namespace AoC2016.Solutions
{
	public class Day05 : AdventOfCodeDay
	{
		public string input = "ffykfhsq";

		public override object Solve1()
		{

			var md5 = MD5.Create();
			string password = "";
			long i = 0;
			while (password.Length < 8)
			{
				var hash = Utilities.GetMd5Hash(md5, $"{input}{i}");
				if (hash.StartsWith("00000", StringComparison.Ordinal))
				{
					password += hash[5];
					//Console.WriteLine(password);
				}
				i++;
				//if (i % 10000 == 0)
				//	Console.WriteLine(i);

			}
			return password;
		}

		public override object Solve2()
		{
			var md5 = MD5.Create();
			char[] password = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
			long i = 0;
			var foundChars = 0;
			while (foundChars < 8)
			{
				var hash = Utilities.GetMd5Hash(md5, $"{input}{i}");
				if (hash.StartsWith("00000", StringComparison.Ordinal))
				{
					if (hash[5] >= '0' && hash[5] <= '7')
					{
						var idx = Convert.ToInt32(hash[5].ToString());
						if (password[idx] == ' ')
						{
							password[idx] = hash[6];
							//Console.WriteLine(string.Concat(password));
							foundChars++;
						}
					}
				}
				i++;
				//if (i % 10000 == 0)
				//	Console.WriteLine(i);

			}
			return string.Concat(password);
		}
	}
}

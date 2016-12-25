using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AoC2016
{
	public class Utilities
	{
		public static string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			return ByteArrayToHexViaLookup32(data);
			//StringBuilder sBuilder = new StringBuilder(); 
			//for (int i = 0; i < data.Length; i++)
			//{
			//	sBuilder.Append(data[i].ToString("x2"));
			//} 
			//return sBuilder.ToString();
		}

		private static readonly uint[] _lookup32 = CreateLookup32();

		private static uint[] CreateLookup32()
		{
			var result = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				string s = i.ToString("x2");
				result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
			}
			return result;
		}

		private static string ByteArrayToHexViaLookup32(byte[] bytes)
		{
			var lookup32 = _lookup32;
			var result = new char[bytes.Length * 2];
			for (int i = 0; i < bytes.Length; i++)
			{
				var val = lookup32[bytes[i]];
				result[2 * i] = (char)val;
				result[2 * i + 1] = (char)(val >> 16);
			}
			return new string(result);
		}

		public static List<List<T>> GeneratePermutations<T>(List<T> items)
		{
			// Make an array to hold the
			// permutation we are building.
			T[] current_permutation = new T[items.Count];

			// Make an array to tell whether
			// an item is in the current selection.
			bool[] in_selection = new bool[items.Count];

			// Make a result list.
			List<List<T>> results = new List<List<T>>();

			// Build the combinations recursively.
			PermuteItems(items, in_selection,
				current_permutation, results, 0);

			// Return the results.
			return results;
		}

		// Recursively permute the items that are
		// not yet in the current selection.
		public static void PermuteItems<T>(List<T> items, bool[] in_selection,
			T[] current_permutation, List<List<T>> results,
			int next_position)
		{
			// See if all of the positions are filled.
			if (next_position == items.Count)
			{
				// All of the positioned are filled.
				// Save this permutation.
				results.Add(current_permutation.ToList());
			}
			else
			{
				// Try options for the next position.
				for (int i = 0; i < items.Count; i++)
				{
					if (!in_selection[i])
					{
						// Add this item to the current permutation.
						in_selection[i] = true;
						current_permutation[next_position] = items[i];

						// Recursively fill the remaining positions.
						PermuteItems(items, in_selection,
							current_permutation, results,
							next_position + 1);

						// Remove the item from the current permutation.
						in_selection[i] = false;
					}
				}
			}
		}

	}
}


using System;
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

	}
}


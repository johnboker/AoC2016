using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2016.Solutions
{
	public class Day04 : AdventOfCodeDay
	{
		public List<Room> Rooms { get; set; }
		public Day04()
		{
			var input = File.ReadLines("../../input/day4.txt");
			Rooms = input.Select(a => new Room(a)).ToList();

		}
		private List<Room> RealRooms()
		{
			return Rooms.Where(r => r.IsReal()).ToList();
		}

		public override object Solve1()
		{ 
			return RealRooms().Sum(r => r.SectorId); 
		}

		public override object Solve2()
		{
			var room = RealRooms().Where(a => a.RealName() == "northpole-object-storage").FirstOrDefault();
			return room.SectorId;
		}


	}

	public class Room
	{

		public Room(string roomDef)
		{
			//Console.WriteLine(roomDef);

			int idx = roomDef.LastIndexOf('-');
			Name = roomDef.Substring(0, idx);
			int idx2 = roomDef.IndexOf('[');
			SectorId = Convert.ToInt32(roomDef.Substring(idx + 1, idx2 - idx - 1));
			Checksum = roomDef.Substring(idx2 + 1, roomDef.Length - 1 - idx2 - 1);

			//Console.WriteLine($"{Name}, {SectorId}, {Checksum}");
		}

		public string RealName()
		{
			var sb = new StringBuilder();
			foreach (char c in Name)
			{
				if (c == '-') sb.Append(c);
				else sb.Append(RotateCharacter(c, SectorId));
			}
			var name = sb.ToString();
			//if (name.Contains("north"))
			    //Console.WriteLine(name);
			return name;
		}

		private char RotateCharacter(int c, int n)
		{
			c = c - 'a';
			c = (c + n) % 26;
			return (char)(c + 'a');
		}

		public bool IsReal()
		{

			var name = Name.Replace("-", "");

			var g = (from c in name.ToCharArray()
					 group c by c into grp
					 select new { Letter = grp.Key, Count = grp.Count() }).ToList();

			var cs = string.Concat(g.OrderByDescending(a => a.Count).ThenBy(a => a.Letter).Select(a => a.Letter).Take(5));
			//Console.WriteLine(Checksum + ' ' + cs);
			return Checksum == cs;
		}

		public string Name { get; set; }
		public int SectorId { get; set; }
		public string Checksum { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2016.Solutions
{
	public class Day03 : AdventOfCodeDay
	{
		List<Triangle> Triangles { get; set; }
		List<Triangle> Triangles2 { get; set; }
		public Day03()
		{
			var input = File.ReadLines("../../input/day3.txt");
			Triangles = input.Select(a => new Triangle(a.Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).Select(b => Convert.ToInt32(b)).ToArray())).ToList();

			Triangles2 = new List<Triangle>();
			for (int i = 0; i < Triangles.Count; i += 3) {
				var row1 = Triangles[i + 0];
				var row2 = Triangles[i + 1];
				var row3 = Triangles[i + 2];
				Triangles2.Add(new Triangle(new[] { row1.Sides[0], row2.Sides[0], row3.Sides[0] }));
				Triangles2.Add(new Triangle(new[] { row1.Sides[1], row2.Sides[1], row3.Sides[1] }));
				Triangles2.Add(new Triangle(new[] { row1.Sides[2], row2.Sides[2], row3.Sides[2] }));
			}
		}

		public override object Solve1()
		{
			var goodCount = Triangles.Count(f => f.IsValid());
			return goodCount;
		}

		public override object Solve2()
		{
			var goodCount = Triangles2.Count(f => f.IsValid());
			return goodCount;
		}
	}

	public class Triangle
	{
		public int[] Sides { get; set; }

		public Triangle(int[] sides)
		{
			Sides = sides;
		}

		public bool IsValid()
		{
			var permutations = GetPermutations(Sides).Where(a => a.Count() == 3).Select(a => a.ToArray());
			return permutations.All(p => p[0] + p[1] > p[2]);
		}

		public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items)
		{
			if (items.Count() > 1)
			{
				return items.SelectMany(item => GetPermutations(items.Where(i => !i.Equals(item))),
										(item, permutation) => new[] { item }.Concat(permutation));
			}
			else
			{
				return new[] { items };
			}
		}
	}
}


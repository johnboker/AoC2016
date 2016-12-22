using System;
using System.Diagnostics;
using System.Linq;

namespace AoC2016
{
	class MainClass
	{
		public static void Main()
		{
			var type = typeof(AdventOfCodeDay);
			var types = AppDomain.CurrentDomain.GetAssemblies()
								 .SelectMany(s => s.GetTypes())
								 .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
								 .OrderBy(a => a.FullName);

			Console.Write("Please enter the day number to run: ");
			var dayToShow = Console.ReadLine();
			 

			if (string.IsNullOrEmpty(dayToShow))
			{
				dayToShow = DateTime.Now.Day.ToString("D2");
			}

			dayToShow = $"Day{dayToShow}";

			var day = types.Where(t => t.Name == dayToShow).Select(t => (AdventOfCodeDay)Activator.CreateInstance(t)).FirstOrDefault();

			if (day != null)
			{
				Console.WriteLine($"=================== {day.GetType().Name} ===================");
				var stopwatch = new Stopwatch();
				stopwatch.Start();
				Console.WriteLine($"Part 1: {day.Solve1()}");
				stopwatch.Stop();
				Console.WriteLine($" -- Elapsed Time > {stopwatch.Elapsed}");
				stopwatch.Restart();
				Console.WriteLine($"Part 2: {day.Solve2()}");
				stopwatch.Stop();
				Console.WriteLine($" -- Elapsed Time > {stopwatch.Elapsed}");
				Console.WriteLine();
			}
		}
	}
}

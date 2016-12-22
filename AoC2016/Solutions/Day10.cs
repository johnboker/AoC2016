using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day10 : AdventOfCodeDay
	{
		public List<Bot> Bots { get; set; }
		public List<Output> Outputs { get; set; }

		public Day10()
		{
			var input = File.ReadLines("../../input/day10.txt");
			Parse(input);
		}


		public override object Solve1()
		{
			while (Bots.Any(a => !a.HasBothMicrochips))
			{
				foreach (var b in Bots)
				{
					if (b.HasBothMicrochips)
					{
						b.LowReceiverBot?.AddValue(b.Microchips.Min());
						b.LowReceiverOutput?.AddValue(b.Microchips.Min());
						b.HighReceiverBot?.AddValue(b.Microchips.Max());
						b.HighReceiverOutput?.AddValue(b.Microchips.Max());
					}
				}
			}

			return Bots.FirstOrDefault(a => a.Microchips.Contains(61) && a.Microchips.Contains(17))?.ToString();
		}

		public override object Solve2()
		{
			Solve1();
			Outputs = Outputs.OrderBy(a => a.Number).ToList();
			return Outputs[0].Microchips[0] * Outputs[1].Microchips[0] * Outputs[2].Microchips[0];
		}

		public void Parse(IEnumerable<string> input)
		{
			Bots = new List<Bot>();
			Outputs = new List<Output>();

			foreach (var s in input)
			{
				if (s.StartsWith("value", StringComparison.Ordinal))
				{
					var pattern = new Regex(@"value (?<V>\d+) goes to bot (?<B>\d+)");
					var match = pattern.Match(s);
					var v = int.Parse(match.Groups["V"].Value);
					var b = int.Parse(match.Groups["B"].Value);
					var bot = GetBot(b);
					bot.SetValue(v);
				}
				else
				{
					var pattern = new Regex(@"bot (?<B1>\d+) gives low to (?<R1>\w*) (?<R1n>\d+) and high to (?<R2>\w*) (?<R2n>\d+)");
					var match = pattern.Match(s);
					var b1 = int.Parse(match.Groups["B1"].Value);
					var bot = GetBot(b1);

					var r1 = match.Groups["R1"].Value;
					var r1n = int.Parse(match.Groups["R1n"].Value);

					if (r1 == "bot")
					{
						var rbot1 = GetBot(r1n);
						bot.LowReceiverBot = rbot1;
					}
					else
					{
						var output1 = GetOutput(r1n);
						bot.LowReceiverOutput = output1;
					}

					var r2 = match.Groups["R2"].Value;
					var r2n = int.Parse(match.Groups["R2n"].Value);

					if (r2 == "bot")
					{
						var rbot2 = GetBot(r2n);
						bot.HighReceiverBot = rbot2;
					}
					else
					{
						var output1 = GetOutput(r1n);
						bot.HighReceiverOutput = output1;
					}
				}
			}
		}


		public Bot GetBot(int n)
		{
			var bot = Bots.FirstOrDefault(b => b.Number == n);
			if (bot == null)
			{
				bot = new Bot { Number = n };
				Bots.Add(bot);
			}
			return bot;
		}

		public Output GetOutput(int n)
		{
			var output = Outputs.FirstOrDefault(b => b.Number == n);
			if (output == null)
			{
				output = new Output { Number = n };
				Outputs.Add(output);
			}
			return output;
		}
	}

	public class Output
	{
		public Output() { Microchips = new List<int>(); }

		public int Number { get; set; }
		public List<int> Microchips { get; set; }

		public void AddValue(int val)
		{
			if (Microchips == null) Microchips = new List<int>();
			if (!Microchips.Contains(val))
				Microchips.Add(val);
		}

		public override string ToString()
		{
			return string.Format($"{Number}, Microchips = [{string.Join(", ", Microchips)}]");
		}
	}


	public class Bot : Output
	{
		public bool HasBothMicrochips => Microchips.Count() == 2;

		public Output LowReceiverOutput { get; set; }
		public Output HighReceiverOutput { get; set; }

		public Bot LowReceiverBot { get; set; }
		public Bot HighReceiverBot { get; set; }

		public void SetValue(int val)
		{
			if (Microchips == null) Microchips = new List<int>();
			Microchips.Add(val);
		}
	}
}

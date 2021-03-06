﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions
{
	public class Day12 : AdventOfCodeDay
	{
		List<Command> Commands { get; set; }
		List<Register> Registers { get; set; }

		public Day12()
		{
			
		}

		public int Solve()
		{
			Commands = File.ReadLines("../../input/day12.txt").Select(a=>new Command(a, Registers)).ToList();

			var currentPosition = 0;

			while (currentPosition < Commands.Count())
			{
				var command = Commands[currentPosition];

				switch (command.Instruction)
				{
					case "cpy":
						{
							var v1 = command.Operands[0];
							var v2 = command.Operands[1];
							(v2 as Register).Value = v1.GetType() == typeof(Register) ? (v1 as Register).Value : (int)(v1);
						}
						break;
					case "inc":
						(command.Operands[0] as Register).Value++;
						break;
					case "dec":
						(command.Operands[0] as Register).Value--;
						break;
					case "jnz":
						{
							var v1 = command.Operands[0];
							var v2 = command.Operands[1];
							var tst = v1.GetType() == typeof(Register) ? (v1 as Register).Value : (int)(v1);
							var lines = v2.GetType() == typeof(Register) ? (v2 as Register).Value : (int)(v2);
							if (tst != 0)
							{
								currentPosition += lines;
								continue;
							}
						}
						break;
				}

				currentPosition++;
			}

			return Registers.FirstOrDefault(a => a.Name == "a").Value;
		}

		public override object Solve1()
		{
			Registers = new List<Register> {
				new Day12.Register { Name = "a", Value = 0 },
				 new Day12.Register { Name = "b", Value = 0 },
				 new Day12.Register { Name = "c", Value = 0 },
				 new Day12.Register { Name = "d", Value = 0 }
			};

			return Solve();
		}

		public override object Solve2()
		{
			Registers = new List<Register> {
				new Day12.Register { Name = "a", Value = 0 },
				 new Day12.Register { Name = "b", Value = 0 },
				 new Day12.Register { Name = "c", Value = 1 },
				 new Day12.Register { Name = "d", Value = 0 }
			};
			return Solve();
		}

		public class Command
		{
			private List<Register> Registers { get; set; }
			public string Instruction { get; set; }
			public List<object> Operands { get; set; }

			public Command(string cmd, List<Register> registers)
			{
				Operands = new List<object>();
				Registers = registers;
				var parts = cmd.Split(' ');
				Instruction = parts[0];
				for (int i = 1; i < parts.Length; i++)
				{
					Operands.Add(GetOperand(parts[i]));
				}
			}

			private object GetOperand(string o)
			{
				if (Regex.IsMatch(o, @"^-*[0-9]+$"))
				{
					return Convert.ToInt32(o);
				}
				return Registers.FirstOrDefault(a => a.Name == o);
			}
		}

		public class Register
		{
			public string Name { get; set; }
			public int Value { get; set; }
		}
	}
}


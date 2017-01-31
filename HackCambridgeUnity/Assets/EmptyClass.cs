using System;
using System.Collections.Generic;
using System.Collections;

namespace AssemblyCSharpfirstpass
{
	public class EmptyClass
	{
			public static void Main(string[] args)
			{
				List<int> numbers = new List<int>() { 3, 9, 8, 4, 5, 7, 10 };
				int target = 15;
				sum_up(numbers, target);
			}

			private static void sum_up(List<int> numbers, int target)
			{
				sum_up_recursive(numbers, target, new List<int>());
			}

			private static void sum_up_recursive(List<int> numbers, int target, List<int> partial)
			{
				int s = 0;
				foreach (int x in partial) s += x;

				if (s == target)
					Console.WriteLine("sum(" + partial.ToArray() + ")= " + target);

				if (s >= target)
					return;

				for (int i = 0; i < numbers.Count; i++)
				{
					List<int> remaining = new List<int>();
					int n = numbers[i];
					for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

					List<int> partial_rec = new List<int>(partial);
					partial_rec.Add(n);
					sum_up_recursive(remaining, target, partial_rec);
				}
			}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			T[] elements = source.ToArray();
			for (int i = elements.Length - 1; i > 0; i--)
			{
				int swapIndex = rng.Next(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}

			yield return elements[0];
		}

		public static bool SequenceEqualWithoutOrder<T>(this IEnumerable<T> source, IEnumerable<T> sequence)
		{
			return source.OrderBy(x => x).SequenceEqual(sequence.OrderBy(x => x));
		}
	}
}
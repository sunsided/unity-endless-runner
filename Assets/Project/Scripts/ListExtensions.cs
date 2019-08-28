using System.Collections.Generic;
using JetBrains.Annotations;

namespace Project.Scripts
{
    internal static class ListExtensions
    {
        [NotNull]
        private static readonly System.Random Random = new System.Random();

        public static void Shuffle<T>([NotNull] this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                --n;
                var k = Random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
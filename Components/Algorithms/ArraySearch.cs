using System.Linq;

namespace WordsGuesser.Algorithms
{
    internal static class ArraySearch
    {
        static public int Binary(int[] array, int p, int k, int sz)
        {
            if (array.Length == 0 || p > k) return -1;

            int s = (p + k) / 2;

            if (array[s] == sz) return s;

            if (array[s] < sz) return Binary(array, s + 1, k, sz);
            else return Binary(array, p, s - 1, sz);
        }

        static public int[] FindDuplicatesAroundIndex(int[] array, int pos)
        {
            if (array.Length == 0) return new int[0];

            int i = pos, j = pos;

            while (i < array.Length - 1)
            {
                if (array[++i] != array[pos]) { i--; break; }
            }

            while (j > 0)
            {
                if (array[--j] != array[pos]) { j++; break; }
            }

            int[] indexes = Enumerable.Range(j, i - j + 1).ToArray();

            return indexes;

        }
    }
}

﻿namespace WordsGuesser.Algorithms
{
    internal class MergeSort
    {
        public static int[] runSort(int[] array)
        {
            if (array.Length < 2) return array;

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            for (int i = 0; i < mid; i++)
            {
                left[i] = array[i];
            }

            for (int i = mid; i < array.Length; i++)
            {
                right[i - mid] = array[i];
            }

            runSort(left);
            runSort(right);
            array = merge(left, right, array);

            return array;
        }
        public static int[] merge(int[] left, int[] right, int[] result)
        {
            int i = 0, j = 0;

            for (int index = 0; index < result.Length; index++)
            {
                if (j >= right.Length || (i < left.Length && left[i] <= right[j]))
                {
                    result[index] = left[i++];
                }
                else
                {
                    result[index] = right[j++];
                }
            }

            return result;
        }

    }
}

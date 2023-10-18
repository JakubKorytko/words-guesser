using System;

namespace WordsGuesser.Words
{
    internal static class Unicode
    {
        public static int[] ConvertTo(char[] text)
        {
            int[] result = new int[text.Length];
            int index = 0;

            foreach (char c in text) result[index++] = (int)c;

            return result;
        }

        public static char[] ConvertFrom(int[] codes)
        {
            char[] result = new char[codes.Length];
            int index = 0;

            foreach (int i in codes) result[index++] = Convert.ToChar(i);

            return result;
        }
    }
}

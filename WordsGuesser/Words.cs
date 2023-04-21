using System;

namespace WordsGuesser
{
    internal class Words
    {
        private static string[][] wordList = new string[][] {
            new string[] { "portugal", "countries"},
            new string[] { "dollar", "currencies"},
            new string[] { "giraffe", "animals"},
            new string[] { "tomato", "vegetables"},
            new string[] { "watermelon", "fruits"}
         };

        static public string[] randomWord()
        {
            Random randomGen = new Random();
            int rnd = randomGen.Next(0, wordList.Length);
            return wordList[rnd];
        }

        static public string generateBoard(char[] letters)
        {
            string board = "";
            foreach (char letter in letters)
            {
                board += letter+" ";
            }
            return board;
        }
    }
}

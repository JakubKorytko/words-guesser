using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WordsGuesser.Words
{
    internal class WordsList
    {

        private static Word[] list
        {
            get
            {
                JArray parsedFile = JArray.Parse(File.ReadAllText(@"./Words/words.json"));
                return parsedFile.ToObject<Word[]>();
            }
        }

        static public string[] randomWord()
        {
            Random randomGen = new Random();
            int rnd = randomGen.Next(0, list.Length);
            Word word = list[rnd];
            return new string[] { word.value, word.category};
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
